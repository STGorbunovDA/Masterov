namespace Masterov.Domain.Masterov.Order.DeleteOrder;

public interface IDeleteOrderStorage
{
    Task<bool> DeleteOrder(Guid orderId, CancellationToken cancellationToken);
}