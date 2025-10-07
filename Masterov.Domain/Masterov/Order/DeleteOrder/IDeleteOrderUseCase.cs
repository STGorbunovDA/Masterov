using Masterov.Domain.Masterov.Order.DeleteOrder.Command;

namespace Masterov.Domain.Masterov.Order.DeleteOrder;

public interface IDeleteOrderUseCase
{
    Task<bool> Execute(DeleteOrderCommand deleteOrderCommand, CancellationToken cancellationToken);
}