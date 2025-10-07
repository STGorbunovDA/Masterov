using Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrderStatus;

public interface IUpdateOrderStatusUseCase
{
    Task<OrderDomain> Execute(UpdateOrderStatusCommand updateOrderStatusCommand, CancellationToken cancellationToken);
}