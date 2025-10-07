namespace Masterov.Domain.Masterov.Payment.Service;

public interface IOrderPaymentStatusService
{
    Task UpdateOrderStatus(Guid orderId, CancellationToken cancellationToken);
}