using Masterov.Domain.Masterov.Order.AddOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.AddOrder;

public interface IAddOrderUseCase
{
    Task<OrderDomain> Execute(AddOrderCommand addOrderCommand, CancellationToken cancellationToken);
}