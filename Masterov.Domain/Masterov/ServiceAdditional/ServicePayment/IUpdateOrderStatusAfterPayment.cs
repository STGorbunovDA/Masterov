namespace Masterov.Domain.Masterov.ServiceAdditional.ServicePayment;

public interface IUpdateOrderStatusAfterPayment
{
    Task UpdateOrderStatus(Guid orderId, CancellationToken cancellationToken);
}