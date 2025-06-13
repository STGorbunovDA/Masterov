using AutoMapper;
using Masterov.API.Models.ProductionOrder;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
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
}