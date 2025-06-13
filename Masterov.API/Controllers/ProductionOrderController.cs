using AutoMapper;
using Masterov.API.Models.ProductionOrder;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt.Query;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Ордера (заказы)
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/productionOrder")]
public class ProductionOrderController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех заказах (ордерах)</returns>
    [HttpGet("getProductionOrders")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetProductionOrders(
        [FromServices] IGetProductionOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var files = await useCase.Execute(cancellationToken);
        return Ok(files.Select(mapper.Map<ProductionOrderRequest>));
    }
    
    /// <summary>
    /// Получить ордер (заказ) по Id
    /// </summary>
    /// <param name="orderId">Идентификатор ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказе (ордере)</returns>
    [HttpGet("getProductionOrderById/{orderId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrderById(
        [FromRoute] Guid orderId,
        [FromServices] IGetProductionOrderByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var order = await useCase.Execute(new GetProductionOrderByIdQuery(orderId), cancellationToken);
        return Ok(mapper.Map<ProductionOrderRequest>(order));
    }
    
    /// <summary>
    /// Получить список ордеров (заказов) по дате создания
    /// </summary>
    /// <param name="request">Дата создания ордера (заказа)</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказах (ордерах)</returns>
    [HttpGet("getProductionOrdersByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrdersByCreatedAt(
        [FromQuery] GetProductionOrderByCreatedAtRequest request,
        [FromServices] IGetProductionOrdersByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetProductionOrdersByCreatedAtQuery(request.CreatedAt), cancellationToken);
        return Ok(orders?.Select(mapper.Map<ProductionOrderRequest>) ?? Array.Empty<ProductionOrderRequest>());
    }
    
}