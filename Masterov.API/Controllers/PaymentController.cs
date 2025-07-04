using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductionOrder;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.AddCustomer.Command;
using Masterov.Domain.Masterov.Customer.DeleteCustomer;
using Masterov.Domain.Masterov.Customer.DeleteCustomer.Command;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerById.Query;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Masterov.Customer.GetCustomerByName.Query;
using Masterov.Domain.Masterov.Customer.GetCustomerOrders;
using Masterov.Domain.Masterov.Customer.GetCustomerOrders.Query;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders.Query;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPaymentById.Query;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus.Query;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Платежи
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/payment")]
public class PaymentController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все платежи
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платежах</returns>
    [HttpGet("gePayments")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest[]))]
    [ProducesResponseType(410)]
    //[Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPayments(
        [FromServices] IGetPaymentsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(cancellationToken);
        return Ok(payments.Select(mapper.Map<PaymentRequest>));
    }
    
    /// <summary>
    /// Получить платеж по Id
    /// </summary>
    /// <param name="paymentId">Идентификатор платежа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платеже</returns>
    [HttpGet("getPaymentById/{paymentId:guid}")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPaymentById(
        [FromRoute] Guid paymentId,
        [FromServices] IGetPaymentByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var paymentDomain = await useCase.Execute(new GetPaymentByIdQuery(paymentId), cancellationToken);
        return Ok(mapper.Map<PaymentRequest>(paymentDomain));
    }
    
    /// <summary>
    /// Получить список платежей по статусу
    /// </summary>
    /// <param name="request">Статус платежа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платежах</returns>
    [HttpGet("getPaymentsByStatus")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPaymentsByStatus(
        [FromQuery] GetPaymentsByStatusRequest request,
        [FromServices] IGetPaymentsByStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetPaymentsByStatusQuery(StatusTypeHelper.FromExtensionPaymentMethod(request.Status)), cancellationToken);
        return Ok(payments?.Select(mapper.Map<PaymentRequest>) ?? Array.Empty<PaymentRequest>());
    }
    
    /// <summary>
    /// Получить платежи по дате оплаты
    /// </summary>
    /// <param name="request">Дата оплаты</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платежах</returns>
    [HttpGet("getPaymentsByPaymentDate")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPaymentsByPaymentDate(
        [FromQuery] GetPaymentsByPaymentDateRequest request,
        [FromServices] IGetPaymentsByPaymentDateUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetPaymentsByPaymentDateQuery(request.PaymentDate), cancellationToken);
        return Ok(payments?.Select(mapper.Map<PaymentRequest>) ?? Array.Empty<PaymentRequest>());
    }
    
    /// <summary>
    /// Получить платежи по сумме оплаты
    /// </summary>
    /// <param name="request">Сумма оплаты</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платежах</returns>
    [HttpGet("getPaymentsByAmount")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPaymentsByAmount(
        [FromQuery] GetPaymentsByAmountRequest request,
        [FromServices] IGetPaymentsByAmountUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments = await useCase.Execute(new GetPaymentsByAmountQuery(request.Amount), cancellationToken);
        return Ok(payments?.Select(mapper.Map<PaymentRequest>) ?? Array.Empty<PaymentRequest>());
    }
    
    /// <summary>
    /// Получить заказчика по Идентификатору платежа
    /// </summary>
    /// <param name="request">Идентификатор платежа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о заказчике</returns>
    [HttpGet("getCustomerByPaymentId")]
    [ProducesResponseType(200, Type = typeof(CustomerNewRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomerByPaymentId(
        [FromQuery] GetCustomerByPaymentIdRequest request,
        [FromServices] IGetCustomerByPaymentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetCustomerByPaymentIdQuery(request.PaymentId), cancellationToken);
        return Ok(mapper.Map<CustomerNewRequest>(customer));
    }
    
    /// <summary>
    /// Получить ордер по Идентификатору платежа
    /// </summary>
    /// <param name="request">Идентификатор платежа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о ордере</returns>
    [HttpGet("getProductionOrderByPaymentId")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequestNoPayments))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductionOrderByPaymentId(
        [FromQuery] GetProductionOrderByPaymentIdRequest request,
        [FromServices] IGetProductionOrderByPaymentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var customer = await useCase.Execute(new GetProductionOrderByPaymentIdQuery(request.PaymentId), cancellationToken);
        return Ok(mapper.Map<ProductionOrderRequestNoPayments>(customer));
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
    
        return CreatedAtAction(nameof(GetPaymentById),
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
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
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
    
    /// <summary>
    /// Получить список ордеров заказчика с возможностью фильтрации по Id
    /// </summary>
    /// <param name="request">Идентификатор заказчика</param>
    /// <param name="getCustomerOrdersUseCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Результат получения списка ордеров заказчик</returns>
    [HttpGet("GetCustomerOrders")]
    [ProducesResponseType(200, Type = typeof(ProductionOrderRequestNoCustumer[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetCustomerOrders(
        [FromQuery] GetCustomerOrdersRequest request,
        [FromServices] IGetCustomerOrdersUseCase getCustomerOrdersUseCase,
        CancellationToken cancellationToken)
    {
        var orders = await getCustomerOrdersUseCase.Execute(new GetCustomerOrdersQuery(request.CustomerId), cancellationToken);

        return Ok(orders?.Select(mapper.Map<ProductionOrderRequestNoCustumer>) ?? Array.Empty<ProductionOrderRequestNoCustumer>());
    }
}