using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Payment.DeletePayment.Command;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

namespace Masterov.Domain.Masterov.Payment.DeletePayment;

public class DeletePaymentUseCase(
    IValidator<DeletePaymentCommand> validator,
    IDeletePaymentStorage storage,
    IGetPaymentByIdStorage getPaymentByIdStorage,
    IGetProductionOrderByPaymentIdStorage getProductionOrderByPaymentIdStorage,
    IGetFinishedProductAtOrderStorage getFinishedProductAtOrderStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IUpdateProductionOrderStatusStorage updateProductionOrderStatusStorage
) : IDeletePaymentUseCase
{
    public async Task<bool> Execute(DeletePaymentCommand deletePaymentCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deletePaymentCommand, cancellationToken);
        
        var payment = await getPaymentByIdStorage.GetPaymentById(deletePaymentCommand.PaymentId, cancellationToken)
                      ?? throw new NotFoundByIdException(deletePaymentCommand.PaymentId, "Платеж");
        
        var order = await getProductionOrderByPaymentIdStorage.GetProductionOrderByPaymentId(payment.PaymentId, cancellationToken)
                    ?? throw new InvalidOperationException("Платеж не привязан к заказу (ордеру)");
        
        await UpdateOrderStatusIfNeeded(order.OrderId, deletePaymentCommand.PaymentId, cancellationToken);

        return await storage.DeletePayment(deletePaymentCommand.PaymentId, cancellationToken);;
    }

    private async Task UpdateOrderStatusIfNeeded(Guid orderId, Guid paymentId, CancellationToken ct)
    {
        var finishedProduct = await getFinishedProductAtOrderStorage.GetFinishedProductAtOrder(orderId, ct);
        var productPrice = finishedProduct?.Price ?? 0;

        if (productPrice <= 0)
            return;

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(orderId, ct);
        
        var totalPaid = payments?.Where(payment => payment.PaymentId != paymentId).Sum(payment => payment.Amount) ?? 0;

        var status = GetTargetStatus(productPrice, totalPaid);

        var updatedOrder = await updateProductionOrderStatusStorage.UpdateProductionOrderStatus(orderId, status, ct);

        if (updatedOrder is null)
            throw new InvalidOperationException("Не удалось обновить статус заказа после оплаты");
    }

    private static ProductionOrderStatus GetTargetStatus(decimal productPrice, decimal totalPaid)
    {
        if (totalPaid == 0)
            return ProductionOrderStatus.Draft;

        return productPrice <= totalPaid
            ? ProductionOrderStatus.InProgress
            : ProductionOrderStatus.Partial;
    }
}