using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders;

public interface IGetFinishedProductsByTypeWithoutOrdersUseCase
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(GetFinishedProductsByTypeWithoutOrdersQuery finishedProductsByTypeQuery, CancellationToken cancellationToken);
}