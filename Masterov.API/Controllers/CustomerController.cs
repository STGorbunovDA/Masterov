using AutoMapper;
using Masterov.API.Models.Customer;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.AddCustomer.Command;
using Masterov.Domain.Masterov.Customer.DeleteCustomer;
using Masterov.Domain.Masterov.Customer.DeleteCustomer.Command;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerById.Query;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Masterov.Customer.GetCustomerByName.Query;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Готовое мебельное изделие
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/customer")]
public class CustomerController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить всех заказчиков
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчиках</returns>
    [HttpGet("getCustomers")]
    [ProducesResponseType(200, Type = typeof(CustomerRequest[]))]
    [ProducesResponseType(410)]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomers(
        [FromServices] IGetCustomersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customers = await useCase.Execute(cancellationToken);
        return Ok(customers.Select(mapper.Map<CustomerRequest>));
    }
    
    /// <summary>
    /// Получить заказчика по Id
    /// </summary>
    /// <param name="customerId">Идентификатор заказчика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerById/{customerId:guid}")]
    [ProducesResponseType(200, Type = typeof(CustomerRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerById(
        [FromRoute] Guid customerId,
        [FromServices] IGetCustomerByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByIdQuery(customerId), cancellationToken);
        return Ok(mapper.Map<CustomerRequest>(customer));
    }
    
    /// <summary>
    /// Получить заказчика по имени
    /// </summary>
    /// <param name="customerName">Имя заказчика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("GetCustomerByName/{customerName}")]
    [ProducesResponseType(200, Type = typeof(CustomerRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerByName(
        [FromRoute] string customerName,
        [FromServices] IGetCustomerByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType =
            await useCase.Execute(new GetCustomerByNameQuery(customerName), cancellationToken);
        return Ok(mapper.Map<CustomerRequest>(productType));
    }
    
    /// <summary>
    /// Добавить заказчика
    /// </summary>
    /// <param name="request">Данные о заказчике</param>
    /// <param name="useCase">Сценарий добавления заказчика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addCustomer")]
    [ProducesResponseType(201, Type = typeof(CustomerRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddCustomer(
        [FromForm] AddCustomerRequest request,
        [FromServices] IAddCustomerUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new AddCustomerCommand(request.Name, request.Email, request.Phone), cancellationToken);
    
        return CreatedAtAction(nameof(GetCustomerById),
            new { customerId = customer.CustomerId },
            mapper.Map<CustomerRequest>(customer));
    }
    
    /// <summary>
    /// Удаление заказчика по Id.
    /// </summary>
    /// <param name="customerId">Идентификатор заказчика.</param>
    /// <param name="useCase">Сценарий удаления заказчика.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с кодом 204, если файл был успешно удален.</returns>
    [HttpDelete("deleteCustomer")]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeleteCustomer(
        Guid customerId,
        [FromServices] IDeleteCustomerUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeleteCustomerCommand(customerId), cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить заказчика по Id
    /// </summary>
    /// <param name="request">Данные для обновления заказчика</param>
    /// <param name="useCase">Сценарий обновления заказчика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления заказчика</returns>
    [HttpPatch("updateCustomer")]
    [ProducesResponseType(200, Type = typeof(CustomerRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateCustomer(
        [FromForm] UpdateCustomerRequest request,
        [FromServices] IUpdateCustomerUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateCustomer = await useCase.Execute(
            new UpdateCustomerCommand(request.CustomerId, request.Name, request.Email, request.Phone),
            cancellationToken);
        return Ok(mapper.Map<CustomerRequest>(updateCustomer));
    }
}