namespace Masterov.Domain.Masterov.Payment.ServicePaymentAdditional;

public interface IOrderPaymentStatusService
{
    Task UpdateOrderStatus(Guid orderId, CancellationToken cancellationToken);
}