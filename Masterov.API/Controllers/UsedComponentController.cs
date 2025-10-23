using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Order;
using Masterov.API.Models.ProductType;
using Masterov.API.Models.UsedComponent;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.UsedComponent.GetComponents;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt.Query;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Используемые компоненты
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/usedComponents")]
public class UsedComponentController(IMapper mapper) : ControllerBase
{
    // TODO если статус Canceled тогда все компоненты должны вернуться на склад с которого взяли и соответсвенно?
    // TODO добавить контроллер используемых компонентов и соответственно когда компоненты используют вычитать со склада то кол-во которые используются
    /// <summary>
    /// Получить все используемые компоненты
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о компонентах</returns>
    [HttpGet("getUsedComponents")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponents(
        [FromServices] IGetUsedComponentsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(cancellationToken);
        return Ok(usedComponents.Select(mapper.Map<UsedComponentResponse>));
    }
    
    /// <summary>
    /// Получить используемый компонент по Id
    /// </summary>
    /// <param name="usedComponentId">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемом компоненте</returns>
    [HttpGet("getUsedComponentIdById/{usedComponentId:guid}")]
    [ProducesResponseType(200, Type = typeof(UsedComponentResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentId(
        [FromRoute] Guid usedComponentId,
        [FromServices] IGetUsedComponentByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponent = await useCase.Execute(new GetUsedComponentByIdQuery(usedComponentId), cancellationToken);
        return Ok(mapper.Map<UsedComponentResponse>(usedComponent));
    }

    /// <summary>
    /// Получить используемые компоненты по количеству
    /// </summary>
    /// <param name="request">Количество используемых компонентов</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемых компонентах</returns>
    [HttpGet("getUsedComponentsByQuantity")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByQuantity(
        [FromQuery] UsedComponentsByQuantityRequest request,
        [FromServices] IGetUsedComponentsByQuantityUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByQuantityQuery(request.Quantity), cancellationToken);
        return Ok(usedComponents.Select(mapper.Map<UsedComponentResponse>));
    }
    
    /// <summary>
    /// Получить используемые компоненты по дате создания
    /// </summary>
    /// <param name="request">Дата создания используемых компонентов</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемых компонентах</returns>
    [HttpGet("getUsedComponentsByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByCreatedAt(
        [FromQuery] GetUsedComponentsByCreatedAtRequest request,
        [FromServices] IGetUsedComponentsByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(usedComponents?.Select(mapper.Map<UsedComponentResponse>) ?? Array.Empty<UsedComponentResponse>());
    }
    
    /// <summary>
    /// Получить используемые компоненты по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления используемых компонентов</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о используемых компонентах</returns>
    [HttpGet("getUsedComponentsByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByUpdatedAt(
        [FromQuery] GetUsedComponentsByUpdatedAtRequest request,
        [FromServices] IGetUsedComponentsByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(usedComponents?.Select(mapper.Map<UsedComponentResponse>) ?? Array.Empty<UsedComponentResponse>());
    }

    /// <summary>
    /// Получить заказ по идентификатору используемого компонента
    /// </summary>
    /// <param name="request">Идентификатор используемого компонента</param>
    /// <param name="getOrderByUsedComponentIdUseCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Результат получения заказа</returns>
    [HttpGet("getOrderByUsedComponentId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetOrderByUsedComponentId(
        [FromQuery] GetOrderByUsedComponentIdRequest request,
        [FromServices] IGetOrderByUsedComponentIdUseCase getOrderByUsedComponentIdUseCase,
        CancellationToken cancellationToken)
    {
        var usedComponent = await getOrderByUsedComponentIdUseCase.Execute(new GetOrderByUsedComponentIdQuery(request.UsedComponentId), cancellationToken);
        return Ok(mapper.Map<OrderResponse>(usedComponent));
    }
    
    /// <summary>
    /// Получить тип продукта используемого компонента ппо идентификатору используемого компонента
    /// </summary>
    /// <param name="request">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе продука</returns>
    [HttpGet("getProductTypeByUsedComponentId")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetProductTypeByUsedComponentId(
        [FromQuery] GetProductTypeByUsedComponentIdRequest request,
        [FromServices] IGetProductTypeByUsedComponentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productTypeDomain = await useCase.Execute(new GetProductTypeByUsedComponentIdQuery(request.UsedComponentId), cancellationToken);
        return Ok(mapper.Map<ProductTypeResponse>(productTypeDomain));
    }
    
    /// <summary>
    /// Получить склад используемого компонента по идентификатору используемого компонента
    /// </summary>
    /// <param name="request">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("getWarehouseByUsedComponentId")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouseByUsedComponentId(
        [FromQuery] GetWarehouseByUsedComponentIdRequest request,
        [FromServices] IGetWarehouseByUsedComponentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouseDomain = await useCase.Execute(new GetWarehouseByUsedComponentIdQuery(request.UsedComponentId), cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(warehouseDomain));
    }
}