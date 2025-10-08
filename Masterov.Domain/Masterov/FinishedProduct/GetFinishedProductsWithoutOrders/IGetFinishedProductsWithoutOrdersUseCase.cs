using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;

public interface IGetFinishedProductsWithoutOrdersUseCase
{
    Task<IEnumerable<FinishedProductWithoutOrdersDomain>> Execute(CancellationToken cancellationToken);
}