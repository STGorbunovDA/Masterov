using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders;

public interface IGetFinishedProductsByNameWithoutOrdersUseCase
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(GetFinishedProductsByNameWithoutOrdersQuery finishedProductsByNameQuery, CancellationToken cancellationToken);
}