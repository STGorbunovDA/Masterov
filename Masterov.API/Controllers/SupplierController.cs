using AutoMapper;
using Masterov.API.Models.Supplier;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Поставщик
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/supplier")]
public class SupplierController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить всех заказчиков
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчиках</returns>
    [HttpGet("getSuppliers")]
    [ProducesResponseType(200, Type = typeof(SupplierRequest[]))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliers(
        [FromServices] IGetSuppliersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(cancellationToken);
        return Ok(suppliers.Select(mapper.Map<SupplierRequest>));
    }
    
}