using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;

namespace Masterov.Domain.Masterov.Payment.Service;

public class OrderPaymentStatusService(
    IGetFinishedProductByOrderIdStorage getFinishedProductByOrderIdStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IUpdateOrderStatusStorage updateOrderStatusStorage)
    : IOrderPaymentStatusService
{
    public async Task UpdateOrderStatus(Guid orderId, CancellationToken cancellationToken)
    {
        var finishedProduct = await getFinishedProductByOrderIdStorage.GetFinishedProductByOrderId(orderId, cancellationToken);
        var productPrice = finishedProduct?.Price ?? 0;

        if (productPrice <= 0)
            return;

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(orderId, cancellationToken);
        var totalPaid = payments?.Sum(p => p.Amount) ?? 0;

        var newStatus = GetTargetStatus(productPrice, totalPaid);

        var updatedOrder = await updateOrderStatusStorage.UpdateOrderStatus(orderId, newStatus, cancellationToken);
        if (updatedOrder is null)
            throw new InvalidOperationException("Не удалось обновить статус заказа после оплаты");
    }

    private static OrderStatus GetTargetStatus(decimal productPrice, decimal totalPaid)
    {
        if (totalPaid == 0)
            return OrderStatus.Draft;

        return productPrice <= totalPaid
            ? OrderStatus.InProgress
            : OrderStatus.Partial;
    }
}