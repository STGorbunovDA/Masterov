using AutoMapper;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.Domain.Masterov.Supplier.AddSupplier;
using Masterov.Domain.Masterov.Supplier.AddSupplier.Command;
using Masterov.Domain.Masterov.Supplier.DeleteSupplier;
using Masterov.Domain.Masterov.Supplier.DeleteSupplier.Command;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.GetSupplierById.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone.Query;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName.Query;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname.Query;
using Masterov.Domain.Masterov.Supplier.UpdateSupplier;
using Masterov.Domain.Masterov.Supplier.UpdateSupplier.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Поставщик
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/suppliers")]
public class SupplierController(IMapper mapper) : ControllerBase
{
    // TODO при добавлении поставщика имя должно быть уникальным
    
    /// <summary>
    /// Получить всех поставщиков
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщиках</returns>
    [HttpGet("getSuppliers")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliers(
        [FromServices] IGetSuppliersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(cancellationToken);
        return Ok(suppliers?.Select(mapper.Map<SupplierResponse>) ?? Array.Empty<SupplierResponse>());
    }
    
    /// <summary>
    /// Получить поставщика по Id
    /// </summary>
    /// <param name="supplierId">Идентификатор поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("getSupplierById/{supplierId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierById(
        [FromRoute] Guid supplierId,
        [FromServices] IGetSupplierByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByIdQuery(supplierId), cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Получить поставщиков по имени
    /// </summary>
    /// <param name="supplierName">Имя поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщиках</returns>
    [HttpGet("GetSuppliersByName/{supplierName}")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliersByName(
        [FromRoute] string supplierName,
        [FromServices] IGetSuppliersByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(new GetSuppliersByNameQuery(supplierName), cancellationToken);
        return Ok(suppliers?.Select(mapper.Map<SupplierResponse>) ?? Array.Empty<SupplierResponse>());
    }
    
    /// <summary>
    /// Получить поставщиков по фамилии
    /// </summary>
    /// <param name="supplierSurname">Фамилия поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщиках</returns>
    [HttpGet("getSuppliersBySurname/{supplierSurname}")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliersBySurname(
        [FromRoute] string supplierSurname,
        [FromServices] IGetSuppliersBySurnameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(new GetSuppliersBySurnameQuery(supplierSurname), cancellationToken);
        return Ok(suppliers?.Select(mapper.Map<SupplierResponse>) ?? Array.Empty<SupplierResponse>());
    }
    
    /// <summary>
    /// Получить потавщика по телефону
    /// </summary>
    /// <param name="supplierPhone">Телефон поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierByPhone/{supplierPhone}")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByPhone(
        [FromRoute] string supplierPhone,
        [FromServices] IGetSupplierByPhoneUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByPhoneQuery(supplierPhone), cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Получить потавщика по адресу
    /// </summary>
    /// <param name="supplierAddress">адрес поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierByAdress/{supplierAddress}")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByAddress(
        [FromRoute] string supplierAddress,
        [FromServices] IGetSupplierByAddressUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByAddressQuery(supplierAddress), cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Получить список поставок поставщика
    /// </summary>
    /// <param name="request">Идентификатор поставщика</param>
    /// <param name="getNewSuppliesBySupplierIdUseCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Результат получения списка поставок поставщика</returns>
    [HttpGet("GetNewSuppliesBySupplierId")]
    [ProducesResponseType(200, Type = typeof(SupplyNewResponse[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetNewSuppliesBySupplierId(
        [FromQuery] GetSuppliesBySupplierIdRequest request,
        [FromServices] IGetNewSuppliesBySupplierIdUseCase getNewSuppliesBySupplierIdUseCase,
        CancellationToken cancellationToken)
    {
        var supplies = await getNewSuppliesBySupplierIdUseCase.Execute(new GetNewSuppliesBySupplierIdQuery(request.SupplierId), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Добавить потавщика
    /// </summary>
    /// <param name="request">Данные о поставщике</param>
    /// <param name="useCase">Сценарий добавления поставщика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addSupplier")]
    [ProducesResponseType(201, Type = typeof(SupplierResponse))]
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
            mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Удаление поставщика по Id.
    /// </summary>
    /// <param name="supplierId">Идентификатор поставщика.</param>
    /// <param name="useCase">Сценарий удаления поставщика.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с кодом 204, если поставщик был успешно удален.</returns>
    [HttpDelete("deleteSupplier")]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteSupplier(
        Guid supplierId,
        [FromServices] IDeleteSupplierUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteSupplierCommand(supplierId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить поставщика по Id
    /// </summary>
    /// <param name="request">Данные для обновления поставщика</param>
    /// <param name="useCase">Сценарий обновления поставщика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateSupplier")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateSupplier(
        [FromForm] UpdateSupplierRequest request,
        [FromServices] IUpdateSupplierUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateSupplier = await useCase.Execute(
            new UpdateSupplierCommand(request.SupplierId, request.Name, request.Address, request.Phone),
            cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(updateSupplier));
    }
}