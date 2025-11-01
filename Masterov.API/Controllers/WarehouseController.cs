using AutoMapper;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouses;
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
[Route("api/warehouse")]
public class WarehouseController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все склады
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складах</returns>
    [HttpGet("getWarehouses")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse[]))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouses(
        [FromServices] IGetWarehousesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouses = await useCase.Execute(cancellationToken);
        return Ok(warehouses.Select(mapper.Map<WarehouseResponse>));
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
    [ProducesResponseType(200, Type = typeof(WarehouseResponse[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetWarehouseByName(
        [FromRoute] string nameWarehouse,
        [FromServices] IGetWarehouseByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouses =
            await useCase.Execute(new GetWarehouseByNameQuery(nameWarehouse), cancellationToken);
        return Ok(warehouses.Select(mapper.Map<WarehouseResponse>));
    }
    
    /// <summary>
    /// Получить поставки по идентификатору склада
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
        return Ok(mapper.Map<IEnumerable<SupplyNoWarehouseNewResponse>>(supplies));
    }
    
    /// <summary>
    /// Обновить склад по Id
    /// </summary>
    /// <param name="request">Данные для обновления склада</param>
    /// <param name="useCase">Сценарий обновления скалад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateWarehouse")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateWarehouse(
        [FromForm] UpdateWarehouseRequest request,
        [FromServices] IUpdateWarehouseUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateWarehouse = await useCase.Execute(
            new UpdateWarehouseCommand(request.WarehouseId, request.ComponentTypeId, request.Name, request.Quantity, request.Price),
            cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(updateWarehouse));
    }
    
    /// <summary>
    /// Обновить количество на складе по Id
    /// </summary>
    /// <param name="request">Данные для обновления склада</param>
    /// <param name="useCase">Сценарий обновления скалад</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateQuantityWarehouseById")]
    [ProducesResponseType(200, Type = typeof(WarehouseResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateQuantityWarehouseById(
        [FromForm] UpdateQuantityWarehouseByIdRequest request,
        [FromServices] IUpdateQuantityWarehouseByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateWarehouse = await useCase.Execute(
            new UpdateQuantityWarehouseByIdCommand(request.WarehouseId, request.Quantity), cancellationToken);
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
        [FromBody] UpdatePriceWarehouseByIdRequest request,
        [FromServices] IUpdatePriceWarehouseByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateWarehouse = await useCase.Execute(
            new UpdatePriceWarehouseByIdCommand(warehouseId, request.Price), cancellationToken);
        return Ok(mapper.Map<WarehouseResponse>(updateWarehouse));
    }
}