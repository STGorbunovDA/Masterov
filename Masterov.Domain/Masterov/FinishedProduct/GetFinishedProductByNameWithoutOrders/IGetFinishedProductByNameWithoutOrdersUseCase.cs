using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders;

public interface IGetFinishedProductByNameWithoutOrdersUseCase
{
    Task<FinishedProductWithoutOrdersDomain?> Execute(GetFinishedProductByNameWithoutOrdersQuery finishedProductByNameQuery, CancellationToken cancellationToken);
}