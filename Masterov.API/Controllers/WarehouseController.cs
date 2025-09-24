using AutoMapper;
using Masterov.API.Models.ProductType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Masterov.Supply.AddSupply;
using Masterov.Domain.Masterov.Supply.AddSupply.Command;
using Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId;
using Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId.Query;
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
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById.Query;
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
    /// Получить склад по Id
    /// </summary>
    /// <param name="warehouseId">Идентификатор склада</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о складе</returns>
    [HttpGet("getWarehouseById/{warehouseId:guid}")]
    [ProducesResponseType(200, Type = typeof(WarehouseRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetWarehouseById(
        [FromRoute] Guid warehouseId,
        [FromServices] IGetWarehouseByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var warehouse = await useCase.Execute(new GetWarehouseByIdQuery(warehouseId), cancellationToken);
        return Ok(mapper.Map<WarehouseRequest>(warehouse));
    }
}