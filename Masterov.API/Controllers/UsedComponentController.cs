using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Order;
using Masterov.API.Models.UsedComponent;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;
using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent.Command;
using Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponents;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt.Query;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent.Command;
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
    /// <summary>
    /// Получить все используемые компоненты
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о компонентах</returns>
    [HttpGet("getUsedComponents")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponents(
        [FromServices] IGetUsedComponentsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(cancellationToken);
        return Ok(usedComponents?.Select(mapper.Map<UsedComponentResponse>) ?? Array.Empty<UsedComponentResponse>());
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
    public async Task<IActionResult> GetUsedComponentById(
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
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByQuantity(
        [FromQuery] UsedComponentsByQuantityRequest request,
        [FromServices] IGetUsedComponentsByQuantityUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents =
            await useCase.Execute(new GetUsedComponentsByQuantityQuery(request.Quantity), cancellationToken);
        return Ok(usedComponents?.Select(mapper.Map<UsedComponentResponse>) ?? Array.Empty<UsedComponentResponse>());
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
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByCreatedAt(
        [FromQuery] GetUsedComponentsByCreatedAtRequest request,
        [FromServices] IGetUsedComponentsByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents =
            await useCase.Execute(new GetUsedComponentsByCreatedAtQuery(request.CreatedAt.ToDateTime()),
                cancellationToken);
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
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByUpdatedAt(
        [FromQuery] GetUsedComponentsByUpdatedAtRequest request,
        [FromServices] IGetUsedComponentsByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents =
            await useCase.Execute(new GetUsedComponentsByUpdatedAtQuery(request.UpdatedAt.ToDateTime()),
                cancellationToken);
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
        var usedComponent =
            await getOrderByUsedComponentIdUseCase.Execute(new GetOrderByUsedComponentIdQuery(request.UsedComponentId),
                cancellationToken);
        return Ok(mapper.Map<OrderResponse>(usedComponent));
    }

    /// <summary>
    /// Получить тип продукта используемого компонента по идентификатору используемого компонента
    /// </summary>
    /// <param name="request">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе продука</returns>
    [HttpGet("getComponentTypeByUsedComponentId")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetComponentTypeByUsedComponentId(
        [FromQuery] GetComponentTypeByUsedComponentIdRequest request,
        [FromServices] IGetComponentTypeByUsedComponentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypeDomain = await useCase.Execute(new GetComponentTypeByUsedComponentIdQuery(request.UsedComponentId),
            cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(componentTypeDomain));
    }

    /// <summary>
    /// Получить склад по идентификатору используемого компонента
    /// </summary>
    /// <param name="request">Идентификатор используемого компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("getWarehouseByUsedComponentId")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouseByUsedComponentId(
        [FromQuery] GetWarehouseByUsedComponentIdRequest request,
        [FromServices] IGetWarehouseByUsedComponentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouseDomain = await useCase.Execute(new GetWarehouseByUsedComponentIdQuery(request.UsedComponentId),
            cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(warehouseDomain));
    }

    /// <summary>
    /// Добавить используемый компонент c учётом общего кол-ва на складе
    /// </summary>
    /// <param name="request">Данные о компоненте</param>
    /// <param name="useCase">Сценарий добавления компонента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addUsedComponent")]
    [ProducesResponseType(201, Type = typeof(UsedComponentResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddUsedComponent(
        [FromBody] AddUsedComponentRequest request,
        [FromServices] IAddUsedComponentUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponent =
            await useCase.Execute(
                new AddUsedComponentCommand(request.OrderId, request.ComponentTypeId, request.WarehouseId,
                    request.Quantity), cancellationToken);

        return CreatedAtAction(nameof(GetUsedComponentById),
            new { usedComponentId = usedComponent.UsedComponentId },
            mapper.Map<UsedComponentResponse>(usedComponent));
    }

    /// <summary>
    /// Удаление используемый компонент по идентификатору
    /// </summary>
    /// <param name="usedComponentId">Идентификатор используемого компонента</param>
    /// <param name="request">Предикат возврат на склад</param>
    /// <param name="useCase">Сценарий удаления заказчика</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Ответ с кодом 204, если заказчик был успешно удален</returns>
    [HttpDelete("deleteUsedComponent/{usedComponentId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteUsedComponent(
        [FromRoute] Guid usedComponentId,
        [FromQuery] DeleteUsedComponentRequest request,
        [FromServices] IDeleteUsedComponentUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteUsedComponentCommand(usedComponentId, request.ReturnWarehouse),
            cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Обновить используемый компонент по идентификатору
    /// </summary>
    /// <param name="usedComponentId">Идентификатор используемого компонента</param>
    /// <param name="request">Данные для обновления используемого компонента</param>
    /// <param name="useCase">Сценарий обновления используемого компонента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления используемого компонента</returns>
    [HttpPatch("updateUsedComponent/{usedComponentId:guid}")]
    [ProducesResponseType(200, Type = typeof(UsedComponentResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [ProducesResponseType(500, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateUsedComponent(
        [FromRoute] Guid usedComponentId,
        [FromBody] UpdateUsedComponentRequest request,
        [FromServices] IUpdateUsedComponentUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateUsedComponent = await useCase.Execute(
            new UpdateUsedComponentCommand(
                usedComponentId, 
                request.OrderId, 
                request.ComponentTypeId, 
                request.WarehouseId,
                request.Quantity,
                request.CreatedAt.ToDateTime()),
            cancellationToken);
        return Ok(mapper.Map<UsedComponentResponse>(updateUsedComponent));
    }
}