using AutoMapper;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.Domain.Masterov.Supplier.AddSupplier;
using Masterov.Domain.Masterov.Supplier.AddSupplier.Command;
using Masterov.Domain.Masterov.Supplier.DeleteSupplier;
using Masterov.Domain.Masterov.Supplier.DeleteSupplier.Command;
using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;
using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.GetSupplierById.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone.Query;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress.Query;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName.Query;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname.Query;
using Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId;
using Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId.Query;
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
   
    /// <summary>
    /// Получить всех поставщиков
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщиках</returns>
    [HttpGet("getSuppliers")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierResponse>))]
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
    /// <param name="request">Имя поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщиках</returns>
    [HttpGet("GetSuppliersByName")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliersByName(
        [FromQuery] GetSuppliersByNameRequest request,
        [FromServices] IGetSuppliersByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(new GetSuppliersByNameQuery(request.SupplierName), cancellationToken);
        return Ok(suppliers?.Select(mapper.Map<SupplierResponse>) ?? Array.Empty<SupplierResponse>());
    }
    
    /// <summary>
    /// Получить поставщиков по фамилии
    /// </summary>
    /// <param name="request">Фамилия поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщиках</returns>
    [HttpGet("getSuppliersBySurname")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliersBySurname(
        [FromQuery] GetSuppliersBySurnameRequest request,
        [FromServices] IGetSuppliersBySurnameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(new GetSuppliersBySurnameQuery(request.SupplierSurname), cancellationToken);
        return Ok(suppliers?.Select(mapper.Map<SupplierResponse>) ?? Array.Empty<SupplierResponse>());
    }
    
    /// <summary>
    /// Получить поставщика по телефону
    /// </summary>
    /// <param name="request">Телефон поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("GetSupplierByPhone")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByPhone(
        [FromQuery] GetSuppliersByPhoneRequest request,
        [FromServices] IGetSupplierByPhoneUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByPhoneQuery(request.SupplierPhone), cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Получить поставщика по почте
    /// </summary>
    /// <param name="request">Почта поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet($"getSupplierByEmail")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSupplierByEmail(
        [FromQuery] GetSupplierByEmailRequest request,
        [FromServices] IGetSupplierByEmailUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new GetSupplierByEmailQuery(request.Email), cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Получить поставщика по адресу
    /// </summary>
    /// <param name="request">Адрес поставщика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о поставщике</returns>
    [HttpGet("getSuppliersByAddress")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplierResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliersByAddress(
        [FromQuery] GetSuppliersByAddressRequest request,
        [FromServices] IGetSuppliersByAddressUseCase useCase,
        CancellationToken cancellationToken)
    {
        var suppliers = await useCase.Execute(new GetSuppliersByAddressQuery(request.SupplierAddress), cancellationToken);
        return Ok(suppliers?.Select(mapper.Map<SupplierResponse>) ?? Array.Empty<SupplierResponse>());
    }
    
    /// <summary>
    /// Получить список поставок поставщика
    /// </summary>
    /// <param name="request">Идентификатор поставщика</param>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Результат получения списка поставок поставщика</returns>
    [HttpGet("getSuppliesBySupplierId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SupplyNewResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetSuppliesBySupplierId(
        [FromQuery] GetSuppliesBySupplierIdRequest request,
        [FromServices] IGetSuppliesBySupplierIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplies = await useCase.Execute(new GetSuppliesBySupplierIdIdQuery(request.SupplierId), cancellationToken);
        return Ok(supplies?.Select(mapper.Map<SupplyNewResponse>) ?? Array.Empty<SupplyNewResponse>());
    }
    
    /// <summary>
    /// Добавить поставщика
    /// </summary>
    /// <param name="request">Данные о поставщике</param>
    /// <param name="useCase">Сценарий добавления поставщика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addSupplier")]
    [ProducesResponseType(201, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddSupplier(
        [FromBody] AddSupplierRequest request,
        [FromServices] IAddSupplierUseCase useCase,
        CancellationToken cancellationToken)
    {
        var supplier = await useCase.Execute(new AddSupplierCommand(request.Name,request.Surname, request.Email, request.Phone, request.Address), cancellationToken);
    
        return CreatedAtAction(nameof(GetSupplierById),
            new { supplierId = supplier.SupplierId },
            mapper.Map<SupplierResponse>(supplier));
    }
    
    /// <summary>
    /// Удаление поставщика по Id. <===
    /// </summary>
    /// <param name="supplierId">Идентификатор поставщика.</param>
    /// <param name="useCase">Сценарий удаления поставщика.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с кодом 204, если поставщик был успешно удален.</returns>
    [HttpDelete("deleteSupplier/{supplierId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteSupplier(
        [FromRoute] Guid supplierId,
        [FromServices] IDeleteSupplierUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteSupplierCommand(supplierId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить поставщика по Id
    /// </summary>
    /// <param name="supplierId">Идентификатор поставщика</param>
    /// <param name="request">Данные для обновления поставщика</param>
    /// <param name="useCase">Сценарий обновления поставщика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления</returns>
    [HttpPatch("updateSupplier/{supplierId:guid}")]
    [ProducesResponseType(200, Type = typeof(SupplierResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateSupplier(
        [FromRoute] Guid supplierId,
        [FromForm] UpdateSupplierRequest request,
        [FromServices] IUpdateSupplierUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateSupplier = await useCase.Execute(
            new UpdateSupplierCommand(supplierId, request.Name, request.Address, request.Phone),
            cancellationToken);
        return Ok(mapper.Map<SupplierResponse>(updateSupplier));
    }
}