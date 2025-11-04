using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Supply;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId.Query;
using Masterov.Domain.Masterov.Warehouse.AddWarehouse;
using Masterov.Domain.Masterov.Warehouse.AddWarehouse.Command;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouses;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt.Query;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById.Command;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById.Command;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Склад
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/warehouses")]
public class WarehouseController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все склады
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складах</returns>
    [HttpGet("getWarehouses")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<WarehouseResponse>))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouses(
        [FromServices] IGetWarehousesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouses = await useCase.Execute(cancellationToken);
        return Ok(warehouses?.Select(mapper.Map<WarehouseResponse>) ?? Array.Empty<WarehouseResponse>());
    }
    
    /// <summary>
    /// Получить склад по Id
    /// </summary>
    /// <param name="warehouseId">Идентификатор склада</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("getWarehouseById/{warehouseId:guid}")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouseById(
        [FromRoute] Guid warehouseId,
        [FromServices] IGetWarehouseByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouse = await useCase.Execute(new GetWarehouseByIdQuery(warehouseId), cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(warehouse));
    }
    
    /// <summary>
    /// Получить склад по имени
    /// </summary>
    /// <param name="nameWarehouse">Название склада</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("getWarehouseByName/{nameWarehouse}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<WarehouseResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetWarehouseByName(
        [FromRoute] string nameWarehouse,
        [FromServices] IGetWarehouseByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouses = await useCase.Execute(new GetWarehouseByNameQuery(nameWarehouse), cancellationToken);
        return Ok(warehouses?.Select(mapper.Map<WarehouseResponse>) ?? Array.Empty<WarehouseResponse>());
    }
    
    /// <summary>
    /// Получить все поставки склада по идентификатору склада
    /// </summary>
    /// <param name="request">Идентификатор склада</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByWarehouseId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNoWarehouseNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByWarehouseId(
        [FromQuery] GetSuppliesByWarehouseIdRequest request,
        [FromServices] IGetSuppliesByWarehouseIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByWarehouseIdQuery(request.WarehouseId), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNoWarehouseNewResponse>) ?? Array.Empty<SupplyNoWarehouseNewResponse>());
    }
    
    /// <summary>
    /// Получить склады по дате создания
    /// </summary>
    /// <param name="request">Дата создания</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складах</returns>
    [HttpGet("getWarehousesByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<WarehouseResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehousesByCreatedAt(
        [FromQuery] GetWarehousesByCreatedAtRequest request,
        [FromServices] IGetWarehousesByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouses = await useCase.Execute(new GetWarehousesByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(warehouses?.Select(mapper.Map<WarehouseResponse>) ?? Array.Empty<WarehouseResponse>());
    }
    
    /// <summary>
    /// Получить склады по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складах</returns>
    [HttpGet("getWarehousesByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<WarehouseResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehousesByUpdatedAt(
        [FromQuery] GetWarehousesByUpdatedAtRequest request,
        [FromServices] IGetWarehousesByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouses = await useCase.Execute(new GetWarehousesByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(warehouses?.Select(mapper.Map<WarehouseResponse>) ?? Array.Empty<WarehouseResponse>());
    }
    
    /// <summary>
    /// Получить тип компонента хранящийся на склале по идентификатору склада
    /// </summary>
    /// <param name="request">Идентификатор склада</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе компонента</returns>
    [HttpGet("getComponentTypeByWarehouseId")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetComponentTypeByWarehouseId(
        [FromQuery] GetComponentTypeByWarehouseIdRequest request,
        [FromServices] IGetComponentTypeByWarehouseIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentType = await useCase.Execute(new GetComponentTypeByWarehouseIdQuery(request.WarehouseId), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(componentType));
    }
    
    /// <summary>
    /// Добавить склад
    /// </summary>
    /// <param name="request">Данные о складе</param>
    /// <param name="useCase">Сценарий добавления склада</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addWarehouse")]
    [ProducesResponseType(201, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddWarehouse(
        [FromBody] AddWarehouseRequest request,
        [FromServices] IAddWarehouseUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouse = await useCase.Execute(new AddWarehouseCommand(request.Name, request.ComponentTypeId), cancellationToken);
    
        if (warehouse is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Склад не создан",
                Detail = "Не удалось создать склад. Проверьте корректность введённых данных."
            });
        }
        
        return CreatedAtAction(nameof(GetWarehouseById),
            new { warehouseId = warehouse.WarehouseId },
            mapper.Map<WarehouseResponse>(warehouse));
    }
    
    /// <summary>
    /// Обновить склад по Id
    /// </summary>
    /// <param name="warehouseId">Идентификатор заказчика</param>
    /// <param name="request">Данные для обновления склада</param>
    /// <param name="useCase">Сценарий обновления скалад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateWarehouse/{warehouseId:guid}")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateWarehouse(
        [FromRoute] Guid warehouseId,
        [FromForm] UpdateWarehouseRequest request,
        [FromServices] IUpdateWarehouseUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateWarehouse = await useCase.Execute(
            new UpdateWarehouseCommand(warehouseId, request.ComponentTypeId, request.Name, request.Quantity, request.Price, request.CreatedAt.ToDateTime()),
            cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(updateWarehouse));
    }
    
    /// <summary>
    /// Обновить количество на складе по Id
    /// </summary>
    /// <param name="warehouseId">Идентификатор склада</param>
    /// <param name="request">Данные для обновления склада</param>
    /// <param name="useCase">Сценарий обновления скалад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateQuantityWarehouseById{warehouseId:guid}")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateQuantityWarehouseById(
        [FromRoute] Guid warehouseId,
        [FromQuery] UpdateQuantityWarehouseByIdRequest request,
        [FromServices] IUpdateQuantityWarehouseByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateWarehouse = await useCase.Execute(
            new UpdateQuantityWarehouseByIdCommand(warehouseId, request.Quantity), cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(updateWarehouse));
    }
    
    /// <summary>
    /// Обновить цену на складе по Id
    /// </summary>
    /// <param name="warehouseId">Идентификатор склада</param>
    /// <param name="request">Данные для обновления склада</param>
    /// <param name="useCase">Сценарий обновления скалад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updatePriceWarehouseById/{warehouseId:guid}")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdatePriceWarehouseById(
        [FromRoute] Guid warehouseId,
        [FromQuery] UpdatePriceWarehouseByIdRequest request,
        [FromServices] IUpdatePriceWarehouseByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateWarehouse = await useCase.Execute(
            new UpdatePriceWarehouseByIdCommand(warehouseId, request.Price), cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(updateWarehouse));
    }
}