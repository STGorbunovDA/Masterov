using AutoMapper;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
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
    [ProducesResponseType(200, Type = typeof(SupplyNewRequest[]))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplies(
        [FromServices] IGetSuppliesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(cancellationToken);
        return Ok(supplies.Select(mapper.Map<SupplyNewRequest>));
    }
    
    /// <summary>
    /// Получить поставку по Id
    /// </summary>
    /// <param name="supplyId">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставке</returns>
    [HttpGet("getSupplyById/{supplyId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplyNewRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplyById(
        [FromRoute] Guid supplyId,
        [FromServices] IGetSupplyByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supply = await useCase.Execute(new GetSupplyByIdQuery(supplyId), cancellationToken);
        return Ok(mapper.Map<SupplyNewRequest>(supply));
    }
    
    /// <summary>
    /// Получить все поставки по количеству запчастей
    /// </summary>
    /// <param name="request">Количество поставленных запчастей</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByQuantity")]
    [ProducesResponseType(200, Type = typeof(SupplyNewRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByQuantity(
        [FromQuery] SuppliesByQuantityRequest request,
        [FromServices] IGetSuppliesByQuantityUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesByQuantityQuery(request.Quantity), cancellationToken);
        return Ok(supplies.Select(mapper.Map<SupplyNewRequest>));
    }
    
    /// <summary>
    /// Получить поставки по цене
    /// </summary>
    /// <param name="request">Сумма оплаты</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesByPriceSupply")]
    [ProducesResponseType(200, Type = typeof(SupplyNewRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesByPriceSupply(
        [FromQuery] GetSuppliesByPriceSupplyRequest request,
        [FromServices] IGetSuppliesByPriceSupplyUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetSuppliesByAmountPriceSupply(request.PriceSupply), cancellationToken);
        return Ok(payments?.Select(mapper.Map<SupplyNewRequest>) ?? Array.Empty<SupplyNewRequest>());
    }
    
    /// <summary>
    /// Получить поставки по дате
    /// </summary>
    /// <param name="request">Дата поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("getSuppliesBySupplyDate")]
    [ProducesResponseType(200, Type = typeof(SupplyNewRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesBySupplyDate(
        [FromQuery] GetSuppliesBySupplyDateRequest request,
        [FromServices] IGetSuppliesBySupplyDateUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetSuppliesBySupplyDateQuery(request.SupplyDate), cancellationToken);
        return Ok(payments?.Select(mapper.Map<SupplyNewRequest>) ?? Array.Empty<SupplyNewRequest>());
    }
    
    /// <summary>
    /// Получить поставщика по идентификатору поставки
    /// </summary>
    /// <param name="request">Идентификатор поставки</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставках</returns>
    [HttpGet("GetSupplierBySupplyId")]
    [ProducesResponseType(200, Type = typeof(SupplierNewRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierBySupplyId(
        [FromQuery] GetSupplierBySupplyIdRequest request,
        [FromServices] IGetSupplierBySupplyIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierBySupplyIdQuery(request.SupplyId), cancellationToken);
        return Ok(mapper.Map<SupplierNewRequest>(supplier));
    }
}