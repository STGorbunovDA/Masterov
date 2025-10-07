using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.DeletePayment;
using Masterov.Domain.Masterov.Payment.DeletePayment.Command;
using Masterov.Domain.Masterov.Payment.GetOrderByPaymentId;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.Service;

public class DeletePaymentUseCase(
    IValidator<DeletePaymentCommand> validator,
    IDeletePaymentStorage storage,
    IGetPaymentByIdStorage getPaymentByIdStorage,
    IGetOrderByPaymentIdStorage getOrderByPaymentIdStorage,
    IOrderPaymentStatusService orderPaymentStatusService)
    : IDeletePaymentUseCase
{
    public async Task<bool> Execute(DeletePaymentCommand deletePaymentCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deletePaymentCommand, cancellationToken);

        var payment = await getPaymentByIdStorage.GetPaymentById(deletePaymentCommand.PaymentId, cancellationToken)
                      ?? throw new NotFoundByIdException(deletePaymentCommand.PaymentId, "Платеж");

        var order = await getOrderByPaymentIdStorage.GetOrderByPaymentId(payment.PaymentId, cancellationToken)
                    ?? throw new InvalidOperationException("Платеж не привязан к заказу");

        var result = await storage.DeletePayment(deletePaymentCommand.PaymentId, cancellationToken);

        // 🔄 после удаления платежа обновляем статус заказа
        await orderPaymentStatusService.UpdateOrderStatus(order.OrderId, cancellationToken);

        return result;
    }
}