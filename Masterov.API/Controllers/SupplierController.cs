using AutoMapper;
using Masterov.API.Models.Customer;
using Masterov.API.Models.ProductionOrder;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;
using Masterov.Domain.Masterov.Supplier.AddSupplier;
using Masterov.Domain.Masterov.Supplier.AddSupplier.Command;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.GetSupplierById.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierByName;
using Masterov.Domain.Masterov.Supplier.GetSupplierByName.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone.Query;
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
    // TODO при добавлении поставщика имя должно быть уникальным
    
    /// <summary>
    /// Получить всех поставщиков
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
    
    /// <summary>
    /// Получить поставщика по Id
    /// </summary>
    /// <param name="supplierId">Идентификатор поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getSupplierById/{supplierId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplierRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierById(
        [FromRoute] Guid supplierId,
        [FromServices] IGetSupplierByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByIdQuery(supplierId), cancellationToken);
        return Ok(mapper.Map<SupplierRequest>(supplier));
    }
    
    /// <summary>
    /// Получить поставщика по имени
    /// </summary>
    /// <param name="supplierName">Имя поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierByName/{supplierName}")]
    [ProducesResponseType(200, Type = typeof(SupplierRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByName(
        [FromRoute] string supplierName,
        [FromServices] IGetSupplierByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier =
            await useCase.Execute(new GetSupplierByNameQuery(supplierName), cancellationToken);
        return Ok(mapper.Map<SupplierRequest>(supplier));
    }
    
    /// <summary>
    /// Получить потавщика по телефону
    /// </summary>
    /// <param name="supplierPhone">Телефон поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierByPhone/{supplierPhone}")]
    [ProducesResponseType(200, Type = typeof(SupplierRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByPhone(
        [FromRoute] string supplierPhone,
        [FromServices] IGetSupplierByPhoneUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByPhoneQuery(supplierPhone), cancellationToken);
        return Ok(mapper.Map<SupplierRequest>(supplier));
    }
    
    /// <summary>
    /// Получить потавщика по адресу
    /// </summary>
    /// <param name="supplierAddress">адрес поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierByAdress/{supplierAddress}")]
    [ProducesResponseType(200, Type = typeof(SupplierRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByAddress(
        [FromRoute] string supplierAddress,
        [FromServices] IGetSupplierByAddressUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByAddressQuery(supplierAddress), cancellationToken);
        return Ok(mapper.Map<SupplierRequest>(supplier));
    }
    
    /// <summary>
    /// Получить список поставок поставщика
    /// </summary>
    /// <param name="request">Идентификатор поставщика</param>
    /// <param name="getNewSuppliesBySupplierIdUseCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Результат получения списка поставок поставщика</returns>
    [HttpGet("GetNewSuppliesBySupplierId")]
    [ProducesResponseType(200, Type = typeof(SupplyNewRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetNewSuppliesBySupplierId(
        [FromQuery] GetSuppliesBySupplierIdRequest request,
        [FromServices] IGetNewSuppliesBySupplierIdUseCase getNewSuppliesBySupplierIdUseCase,
        CancellationToken cancellationToken)
    {
        var supplies = await getNewSuppliesBySupplierIdUseCase.Execute(new GetNewSuppliesBySupplierIdQuery(request.SupplierId), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewRequest>) ?? Array.Empty<SupplyNewRequest>());
    }
    
    /// <summary>
    /// Добавить потавщика
    /// </summary>
    /// <param name="request">Данные о поставщике</param>
    /// <param name="useCase">Сценарий добавления поставщика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addSupplier")]
    [ProducesResponseType(201, Type = typeof(SupplierRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddSupplier(
        [FromForm] AddSupplierRequest request,
        [FromServices] IAddSupplierUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new AddSupplierCommand(request.Name, request.Address, request.Phone), cancellationToken);
    
        return CreatedAtAction(nameof(GetSupplierById),
            new { supplierId = supplier.SupplierId },
            mapper.Map<SupplierRequest>(supplier));
    }
    
}