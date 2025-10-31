﻿using AutoMapper;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.Supply.AddSupply;
using Masterov.Domain.Masterov.Supply.AddSupply.Command;
using Masterov.Domain.Masterov.Supply.DeleteSupply;
using Masterov.Domain.Masterov.Supply.DeleteSupply.Command;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplies;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply.Query;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity.Query;
using Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate;
using Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate.Query;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Supply.GetSupplyById.Query;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.UpdateSupply;
using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Поставка
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/supply")]
public class SupplyController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все поставки
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSupplies")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse[]))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplies(
        [FromServices] IGetSuppliesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(cancellationToken);
        return Ok(supplies.Select(mapper.Map<SupplyNewResponse>));
    }
    
    /// <summary>
    /// Получить поставку по Id
    /// </summary>
    /// <param name="supplyId">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставке</returns>
    [HttpGet("getSupplyById/{supplyId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplyById(
        [FromRoute] Guid supplyId,
        [FromServices] IGetSupplyByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supply = await useCase.Execute(new GetSupplyByIdQuery(supplyId), cancellationToken);
        return Ok(mapper.Map<SupplyNewResponse>(supply));
    }
    
    /// <summary>
    /// Получить все поставки по количеству запчастей
    /// </summary>
    /// <param name="request">Количество поставленных запчастей</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByQuantity")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByQuantity(
        [FromQuery] SuppliesByQuantityRequest request,
        [FromServices] IGetSuppliesByQuantityUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByQuantityQuery(request.Quantity), cancellationToken);
        return Ok(supplies.Select(mapper.Map<SupplyNewResponse>));
    }
    
    /// <summary>
    /// Получить поставки по цене
    /// </summary>
    /// <param name="request">Сумма оплаты</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByPriceSupply")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByPriceSupply(
        [FromQuery] GetSuppliesByPriceSupplyRequest request,
        [FromServices] IGetSuppliesByPriceSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetSuppliesByAmountPriceSupply(request.PriceSupply), cancellationToken);
        return Ok(payments?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить поставки по дате
    /// </summary>
    /// <param name="request">Дата поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesBySupplyDate")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesBySupplyDate(
        [FromQuery] GetSuppliesBySupplyDateRequest request,
        [FromServices] IGetSuppliesBySupplyDateUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetSuppliesBySupplyDateQuery(request.SupplyDate), cancellationToken);
        return Ok(payments?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Получить поставщика по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierBySupplyId")]
    [ProducesResponseType(200, Type = typeof(SupplierNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierBySupplyId(
        [FromQuery] GetSupplierBySupplyIdRequest request,
        [FromServices] IGetSupplierBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<SupplierNewResponse>(supplier));
    }
    
    /// <summary>
    /// Получить тип продукта по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе продука</returns>
    [HttpGet("GetComponentTypeBySupplyId")]
    [ProducesResponseType(200, Type = typeof(ComponentTypeResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetComponentTypeBySupplyId(
        [FromQuery] GetComponentTypeBySupplyIdRequest request,
        [FromServices] IGetComponentTypeBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var componentTypeDomain = await useCase.Execute(new GetComponentTypeBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<ComponentTypeResponse>(componentTypeDomain));
    }
    
    /// <summary>
    /// Получить склад по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("GetWarehouseBySupplyId")]
    [ProducesResponseType(200, Type = typeof(WarehouseNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouseBySupplyId(
        [FromQuery] GetWarehouseBySupplyIdRequest request,
        [FromServices] IGetWarehouseBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouseDomain = await useCase.Execute(new GetWarehouseBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<WarehouseNewResponse>(warehouseDomain));
    }
    
    /// <summary>
    /// Добавить поставку
    /// </summary>
    /// <param name="request">Данные о поставке</param>
    /// <param name="useCase">Сценарий добавления поставки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addSupply")]
    [ProducesResponseType(201, Type = typeof(SupplyNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddSupply(
        [FromForm] AddSupplyRequest request,
        [FromServices] IAddSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supply = await useCase.Execute(new AddSupplyCommand(request.SupplierId, request.ComponentTypeId, request.WarehouseId, request.Quantity, request.PriceSupply), cancellationToken);
    
        return CreatedAtAction(nameof(GetSupplyById),
            new { supplyId = supply.SupplyId },
            mapper.Map<SupplyNewResponse>(supply));
    }
    
    /// <summary>
    /// Удаление поставки по Id.
    /// </summary>
    /// <param name="supplyId">Идентификатор поставки.</param>
    /// <param name="useCase">Сценарий удаления поставки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с кодом 204, если поставка бала успешно удалена.</returns>
    [HttpDelete("deleteSupply")]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteSupply(
        Guid supplyId,
        [FromServices] IDeleteSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteSupplyCommand(supplyId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить поставку по Id
    /// </summary>
    /// <param name="request">Данные для обновления поставки</param>
    /// <param name="useCase">Сценарий обновления поставки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateSupply")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateSupply(
        [FromForm] UpdateSupplyRequest request,
        [FromServices] IUpdateSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateSupply = await useCase.Execute(
            new UpdateSupplyCommand(request.SupplyId, request.SupplierId, request.ComponentTypeId, request.WarehouseId, request.Quantity, request.PriceSupply),
            cancellationToken);
        return Ok(mapper.Map<SupplyNewResponse>(updateSupply));
    }
}