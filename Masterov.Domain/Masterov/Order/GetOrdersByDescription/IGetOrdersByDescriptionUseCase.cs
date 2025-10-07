using Masterov.Domain.Masterov.Order.GetOrdersByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByDescription;

public interface IGetOrdersByDescriptionUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByDescriptionQuery getOrdersByDescriptionQuery, CancellationToken cancellationToken);
}