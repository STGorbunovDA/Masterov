namespace Masterov.Domain.Masterov.Payment.Service;

public interface IOrderPaymentStatusService
{
    Task UpdateOrderStatusAsync(Guid orderId, CancellationToken cancellationToken);
}