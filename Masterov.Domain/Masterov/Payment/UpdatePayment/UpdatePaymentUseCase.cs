using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.Service;
using Masterov.Domain.Masterov.Payment.UpdatePayment;
using Masterov.Domain.Masterov.Payment.UpdatePayment.Command;
using Masterov.Domain.Models;

public class UpdatePaymentUseCase(
    IValidator<UpdatePaymentCommand> validator,
    IUpdatePaymentStorage updatePaymentStorage,
    IGetPaymentByIdStorage getPaymentByIdStorage,
    IGetOrderByOrderIdStorage getGetOrderByOrderIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage,
    IOrderPaymentStatusService orderPaymentStatusService)
    : IUpdatePaymentUseCase
{
    public async Task<PaymentDomain?> Execute(UpdatePaymentCommand updatePaymentCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updatePaymentCommand, cancellationToken);

        var paymentExists = await getPaymentByIdStorage.GetPaymentById(updatePaymentCommand.PaymentId, cancellationToken)
                            ?? throw new NotFoundByIdException(updatePaymentCommand.PaymentId, "Платеж");

        var order = await getGetOrderByOrderIdStorage.GetOrderByOrderId(updatePaymentCommand.OrderId, cancellationToken)
                    ?? throw new NotFoundByIdException(updatePaymentCommand.OrderId, "Заказ");

        var customer = await getCustomerByIdStorage.GetCustomerById(updatePaymentCommand.CustomerId, cancellationToken)
                       ?? throw new NotFoundByIdException(updatePaymentCommand.CustomerId, "Заказчик");

        var paymentUpdate = await updatePaymentStorage.UpdatePayment(
            updatePaymentCommand.PaymentId,
            updatePaymentCommand.OrderId,
            updatePaymentCommand.CustomerId,
            updatePaymentCommand.MethodPayment,
            updatePaymentCommand.Amount,
            updatePaymentCommand.CreatedAt,
            cancellationToken);

        // 🔄 после обновления платежа пересчитываем статус заказа
        await orderPaymentStatusService.UpdateOrderStatus(order.OrderId, cancellationToken);

        return await getPaymentByIdStorage.GetPaymentById(paymentUpdate.PaymentId, cancellationToken);
    }
}