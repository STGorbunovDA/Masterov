using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;

public interface IGetFinishedProductsWithoutOrdersUseCase
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(CancellationToken cancellationToken);
}