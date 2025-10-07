using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.Service;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

namespace Masterov.Domain.Masterov.Payment.Services;

public class OrderPaymentStatusService(
    IGetFinishedProductByOrderIdStorage getFinishedProductByOrderIdStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IUpdateProductionOrderStatusStorage updateProductionOrderStatusStorage)
    : IOrderPaymentStatusService
{
    public async Task UpdateOrderStatusAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var finishedProduct = await getFinishedProductByOrderIdStorage.GetFinishedProductByOrderId(orderId, cancellationToken);
        var productPrice = finishedProduct?.Price ?? 0;

        if (productPrice <= 0)
            return;

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(orderId, cancellationToken);
        var totalPaid = payments?.Sum(p => p.Amount) ?? 0;

        var newStatus = GetTargetStatus(productPrice, totalPaid);

        var updatedOrder = await updateProductionOrderStatusStorage.UpdateProductionOrderStatus(orderId, newStatus, cancellationToken);
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