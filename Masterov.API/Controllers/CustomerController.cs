using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Customer;
using Masterov.API.Models.Order;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.AddCustomer.Command;
using Masterov.Domain.Masterov.Customer.DeleteCustomer;
using Masterov.Domain.Masterov.Customer.DeleteCustomer.Command;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail.Query;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerById.Query;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Masterov.Customer.GetCustomerByName.Query;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone.Query;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt;
using Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt.Query;
using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt;
using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt.Query;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Заказчик
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/customers")]
public class CustomerController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить всех заказчиков
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчиках</returns>
    [HttpGet("getCustomers")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomers(
        [FromServices] IGetCustomersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customers = await useCase.Execute(cancellationToken);
        return Ok(customers.Select(mapper.Map<CustomerResponse>));
    }
    
    /// <summary>
    /// Получить заказчика по Id
    /// </summary>
    /// <param name="customerId">Идентификатор заказчика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerById/{customerId:guid}")]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerById(
        [FromRoute] Guid customerId,
        [FromServices] IGetCustomerByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByIdQuery(customerId), cancellationToken);
        return Ok(mapper.Map<CustomerResponse>(customer));
    }
    
    /// <summary>
    /// Получить заказчика по имени
    /// </summary>
    /// <param name="request">Имя заказчика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerByName")]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerByName(
        [FromQuery] GetCustomerByNameRequest request,
        [FromServices] IGetCustomerByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByNameQuery(request.Name), cancellationToken);
        return Ok(mapper.Map<CustomerResponse>(customer));
    }
    
    /// <summary>
    /// Получить заказчика по телефону
    /// </summary>
    /// <param name="request">Телефон заказчика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerByPhone")]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerByPhone(
        [FromQuery] GetCustomerByPhoneRequest request,
        [FromServices] IGetCustomerByPhoneUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByPhoneQuery(request.Phone), cancellationToken);
        return Ok(mapper.Map<CustomerResponse>(customer));
    }
    
    /// <summary>
    /// Получить заказчика по почте
    /// </summary>
    /// <param name="request">Почта заказчика</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet($"getCustomerByEmail")]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerByEmail(
        [FromQuery] GetCustomerByEmailRequest request,
        [FromServices] IGetCustomerByEmailUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByEmailQuery(request.Email), cancellationToken);
        return Ok(mapper.Map<CustomerResponse>(customer));
    }
    
    /// <summary>
    /// Получить заказчиков по дате создания
    /// </summary>
    /// <param name="request">Дата создания заказчиков</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчиках</returns>
    [HttpGet("getCustomersByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetCustomersByCreatedAt(
        [FromQuery] GetCustomersByCreatedAtRequest request,
        [FromServices] IGetCustomersByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customers = await useCase.Execute(new GetCustomersByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(customers?.Select(mapper.Map<CustomerResponse>) ?? Array.Empty<CustomerResponse>());
    }
    
    /// <summary>
    /// Получить заказчиков по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления заказчиков</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчиках</returns>
    [HttpGet("getCustomersByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetCustomersByUpdatedAt(
        [FromQuery] GetCustomersByUpdatedAtRequest request,
        [FromServices] IGetCustomersByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customers = await useCase.Execute(new GetCustomersByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(customers?.Select(mapper.Map<CustomerResponse>) ?? Array.Empty<CustomerResponse>());
    }
    
    /// <summary>
    /// Получить список ордеров заказчика
    /// </summary>
    /// <param name="request">Идентификатор заказчика</param>
    /// <param name="getOrdersByCustomerIdUseCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Результат получения списка ордеров заказчика</returns>
    [HttpGet("getOrdersByCustomerId")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderNoCustumerResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetOrdersByCustomerId(
        [FromQuery] GetOrdersByCustomerIdRequest request,
        [FromServices] IGetOrdersByCustomerIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var orders = await useCase.Execute(new GetOrdersByCustomerIdQuery(request.CustomerId), cancellationToken);
        return Ok(orders?.Select(mapper.Map<OrderNoCustumerResponse>) ?? Array.Empty<OrderNoCustumerResponse>());
    }
    
    /// <summary>
    /// Добавить заказчика
    /// </summary>
    /// <param name="request">Данные о заказчике</param>
    /// <param name="useCase">Сценарий добавления заказчика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addCustomer")]
    [ProducesResponseType(201, Type = typeof(CustomerResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddCustomer(
        [FromBody] AddCustomerRequest request,
        [FromServices] IAddCustomerUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new AddCustomerCommand(request.Name, request.Email, request.Phone, request.UserId), cancellationToken);
    
        return CreatedAtAction(nameof(GetCustomerById),
            new { customerId = customer.CustomerId },
            mapper.Map<CustomerResponse>(customer));
    }
    
    /// <summary>
    /// Удаление заказчика по Id
    /// </summary>
    /// <param name="customerId">Идентификатор заказчика</param>
    /// <param name="useCase">Сценарий удаления заказчика</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Ответ с кодом 204, если заказчик был успешно удален</returns>
    [HttpDelete("deleteCustomer/{customerId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
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
    /// <param name="customerId">Идентификатор заказчика</param>
    /// <param name="request">Данные для обновления заказчика</param>
    /// <param name="useCase">Сценарий обновления заказчика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления заказчика</returns>
    [HttpPatch("updateCustomer/{customerId:guid}")]
    [ProducesResponseType(200, Type = typeof(CustomerResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdateCustomer(
        [FromRoute] Guid customerId,
        [FromBody] UpdateCustomerRequest request,
        [FromServices] IUpdateCustomerUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateCustomer = await useCase.Execute(
            new UpdateCustomerCommand(customerId, request.Name, request.Email, request.Phone, 
                request.CreatedAt?.ToDateTime()),
            cancellationToken);
        return Ok(mapper.Map<CustomerResponse>(updateCustomer));
    }
}