using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByDescription;

public interface IGetOrdersByDescriptionStorage
{
    Task<IEnumerable<OrderDomain>?> GetOrdersByDescription(string description, CancellationToken cancellationToken);
}