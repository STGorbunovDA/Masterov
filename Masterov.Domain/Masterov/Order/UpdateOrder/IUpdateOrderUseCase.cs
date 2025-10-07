using Masterov.Domain.Masterov.Order.UpdateOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrder;

public interface IUpdateOrderUseCase
{
    Task<OrderDomain> Execute(UpdateOrderCommand command, CancellationToken cancellationToken);
}