using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.ProductType;
using Masterov.API.Models.Supply;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Masterov.ComponentType.AddComponentType;
using Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById.Query;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypes;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt.Query;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription.Query;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName.Query;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt.Query;
using Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId;
using Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId.Query;
using Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId;
using Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId.Query;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Masterov.Domain.Masterov.ProductType.GetProductTypes;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/ProductTypes")]
public class ProductTypesController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все типы готовых мебельных изделий
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Типы готовых мебельных изделий</returns>
    [HttpGet("getProductTypes")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProductTypeResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetProductTypes(
        [FromServices] IGetProductTypesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productTypes = await useCase.Execute(cancellationToken);
        return Ok(productTypes?.Select(mapper.Map<ProductTypeResponse>) ?? Array.Empty<ProductTypeResponse>());
    }
    
    /// <summary>
    /// Получить тип готового мебельного изделия по Id
    /// </summary>
    /// <param name="productTypeId">Идентификатор типа мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе готового мебельного изделия</returns>
    [HttpGet("getProductTypeById/{productTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetProductTypeById(
        [FromRoute] Guid productTypeId,
        [FromServices] IGetProductTypeByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetProductTypeByIdQuery(productTypeId), cancellationToken);
        return Ok(mapper.Map<ProductTypeResponse>(productType));
    }
    
    /// <summary>
    /// Получить типы готового мебельного изделия по имени
    /// </summary>
    /// <param name="request">Название типа готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе готового мебельного изделия</returns>
    [HttpGet("getProductTypesByName")]
    [ProducesResponseType(200, Type = typeof(ProductTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetProductTypesByName(
        [FromQuery] GetProductTypesByNameRequest request,
        [FromServices] IGetProductTypesByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetProductTypesByNameQuery(request.ProductTypeName), cancellationToken);
        return Ok(productType?.Select(mapper.Map<ProductTypeResponse>) ?? Array.Empty<ProductTypeResponse>());
    }
    
    /// <summary>
    /// Получить компонент по дате создания
    /// </summary>
    /// <param name="request">Дата создания компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе компонента</returns>
    [HttpGet("getComponentTypesByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ComponentTypeResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetComponentTypesByCreatedAt(
        [FromQuery] GetComponentTypesByCreatedAtRequest request,
        [FromServices] IGetComponentTypesByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypes = await useCase.Execute(new GetComponentTypesByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(componentTypes?.Select(mapper.Map<ComponentTypeResponse>) ?? Array.Empty<ComponentTypeResponse>());
    }
    
    /// <summary>
    /// Получить компонент по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе компонента</returns>
    [HttpGet("getComponentTypesByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ComponentTypeResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetComponentTypesByUpdatedAt(
        [FromQuery] GetComponentTypesByUpdatedAtRequest request,
        [FromServices] IGetComponentTypesByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypes = await useCase.Execute(new GetComponentTypesByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(componentTypes?.Select(mapper.Map<ComponentTypeResponse>) ?? Array.Empty<ComponentTypeResponse>());
    }
    
    /// <summary>
    /// Получить список типов компонентов по описанию
    /// </summary>
    /// <param name="request">Описание типа компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе компонента</returns>
    [HttpGet("getComponentTypesByDescription")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ComponentTypeResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    public async Task<IActionResult> GetComponentTypesByDescription(
        [FromQuery] GetComponentTypesByDescriptionRequest request,
        [FromServices] IGetComponentTypesByDescriptionUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypes = await useCase.Execute(new GetComponentTypesByDescriptionQuery(request.Description), cancellationToken);
        return Ok(componentTypes?.Select(mapper.Map<ComponentTypeResponse>) ?? Array.Empty<ComponentTypeResponse>());
    }
    
    /// <summary>
    /// Получить используемые компоненты по идентификатору типа компонента
    /// </summary>
    /// <param name="request">Идентификатор компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат получения списка используемых компонентов</returns>
    [HttpGet("getUsedComponentsByComponentTypeId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UsedComponentResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetUsedComponentsByComponentTypeId(
        [FromQuery] GetUsedComponentsByComponentTypeIdRequest request,
        [FromServices] IGetUsedComponentsByComponentTypeIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var usedComponents = await useCase.Execute(new GetUsedComponentsByComponentTypeIdQuery(request.ComponentTypeId), cancellationToken);
        return Ok(usedComponents?.Select(mapper.Map<UsedComponentResponse>) ?? Array.Empty<UsedComponentResponse>());
    }
    
    /// <summary>
    /// Получить поставки по идентификатору типа компонента
    /// </summary>
    /// <param name="request">Идентификатор компонента</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат получения списка поставок</returns>
    [HttpGet("getSuppliesByComponentTypeId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByComponentTypeId(
        [FromQuery] GetSuppliesByComponentTypeIdRequest request,
        [FromServices] IGetSuppliesByComponentTypeIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByComponentTypeIdQuery(request.ComponentTypeId), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Добавить тип изделия
    /// </summary>
    /// <param name="request">Данные о типе изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpPost("addComponentType")]
    [ProducesResponseType(201, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddComponentType(
        [FromBody] AddComponentTypeRequest request,
        [FromServices] IAddComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentType = await useCase.Execute(new AddComponentTypeCommand(request.Name, request.Description), cancellationToken);
        return CreatedAtAction(nameof(GetProductTypeById), new { componentTypeId = componentType.ComponentTypeId }, mapper.Map<ComponentTypeResponse>(componentType));
    }
    
    /// <summary>
    /// Удалить тип изделия по Id
    /// </summary>
    /// <param name="componentTypeId">Идентификатор типа изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация выполения</returns>
    [HttpDelete("deleteComponentType/{componentTypeId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteComponentType(
        [FromRoute] Guid componentTypeId,
        [FromServices] IDeleteComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteComponentTypeCommand(componentTypeId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить тип изделия по Id
    /// </summary>
    /// <param name="componentTypeId">Идентификатор типа изделия</param>
    /// <param name="request">Данные для обновления типа изделия</param>
    /// <param name="useCase">Сценарий обновления типа изделия</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления типа изделия</returns>
    [HttpPatch("updateComponentType/{componentTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateComponentType(
        [FromRoute] Guid componentTypeId,
        [FromBody] UpdateComponentTypeRequest request,
        [FromServices] IUpdateComponentTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updatedComponentType = await useCase.Execute(new UpdateComponentTypeCommand(componentTypeId, request.Name, request.CreatedAt.ToDateTime(), request.Description), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(updatedComponentType));
    }
}
