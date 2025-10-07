using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.Customer;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductionOrder;
using Masterov.Domain.Masterov.Payment.AddPayment;
using Masterov.Domain.Masterov.Payment.AddPayment.Command;
using Masterov.Domain.Masterov.Payment.DeletePayment;
using Masterov.Domain.Masterov.Payment.DeletePayment.Command;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPaymentById.Query;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus.Query;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId.Query;
using Masterov.Domain.Masterov.Payment.UpdatePayment;
using Masterov.Domain.Masterov.Payment.UpdatePayment.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Платежи
/// </summary>
/// <param name="mapper"></param>
[ApiController]
[Route("api/payments")]
public class PaymentController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить все платежи
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о платежах</returns>
    [HttpGet("getPayments")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PaymentRequest>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
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
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize]
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
        var payments =
            await useCase.Execute(
                new GetPaymentsByStatusQuery(EnumTypeHelper.FromExtensionPaymentMethod(request.Status)),
                cancellationToken);
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
    [Authorize]
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
    [Authorize]
    public async Task<IActionResult> GetProductionOrderByPaymentId(
        [FromQuery] GetProductionOrderByPaymentIdRequest request,
        [FromServices] IGetProductionOrderByPaymentIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productOrderDomain =
            await useCase.Execute(new GetProductionOrderByPaymentIdQuery(request.PaymentId), cancellationToken);
        return Ok(mapper.Map<ProductionOrderRequestNoPayments>(productOrderDomain));
    }
    
    /// <summary>
    /// Получить платежи по Идентификатору заказа
    /// </summary>
    /// <param name="request">Идентификатор заказа</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о ордере</returns>
    [HttpGet("getPaymentsByOrderId")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest[]))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> GetPaymentsByOrderId(
        [FromQuery] GetPaymentsByOrderIdRequest request,
        [FromServices] IGetPaymentsByOrderIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payments =
            await useCase.Execute(new GetPaymentsByOrderIdQuery(request.OrderId), cancellationToken);
        return Ok(payments?.Select(mapper.Map<PaymentRequest>) ?? Array.Empty<PaymentRequest>());
    }
    

    /// <summary>
    /// Добавить платеж
    /// </summary>
    /// <param name="request">Данные о заказчике</param>
    /// <param name="useCase">Сценарий добавления заказчика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат выполнения</returns>
    [HttpPost("addPayment")]
    [ProducesResponseType(201, Type = typeof(PaymentRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> AddPayment(
        [FromForm] AddPaymentRequest request,
        [FromServices] IAddPaymentUseCase useCase,
        CancellationToken cancellationToken)
    {
        var payment = await useCase.Execute(
            new AddPaymentCommand(request.OrderId, EnumTypeHelper.FromExtensionPaymentMethod(request.MethodPayment),
                request.Amount, request.NameCustomer, request.EmailCustomer, request.PhoneCustomer), cancellationToken);

        return CreatedAtAction(nameof(GetPaymentById),
            new { paymentId = payment.PaymentId },
            mapper.Map<PaymentRequest>(payment));
    }

    /// <summary>
    /// Удаление платежа по Id.
    /// </summary>
    /// <param name="paymentId">Идентификатор платжа.</param>
    /// <param name="useCase">Сценарий удаления платежа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с кодом 204, если платеж был успешно удален.</returns>
    [HttpDelete("deletePayment")]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> DeletePayment(
        Guid paymentId,
        [FromServices] IDeletePaymentUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(new DeletePaymentCommand(paymentId), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Обновить платеж
    /// </summary>
    /// <param name="request">Данные для обновления платежа</param>
    /// <param name="useCase">Сценарий обновления заказчика</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления платежа</returns>
    [HttpPatch("updatePayment")]
    [ProducesResponseType(200, Type = typeof(PaymentRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin, Manager")]
    public async Task<IActionResult> UpdatePayment(
        [FromForm] UpdatePaymentRequest request,
        [FromServices] IUpdatePaymentUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateCustomer = await useCase.Execute(
            new UpdatePaymentCommand(request.PaymentId, request.OrderId, request.CustomerId, 
                EnumTypeHelper.FromExtensionPaymentMethod(request.MethodPayment), 
                request.Amount, request.PaymentDate), cancellationToken);
        return Ok(mapper.Map<PaymentRequest>(updateCustomer));
    }
}