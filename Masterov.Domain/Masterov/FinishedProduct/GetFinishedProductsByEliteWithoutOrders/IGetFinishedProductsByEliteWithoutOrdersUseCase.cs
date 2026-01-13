using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByEliteWithoutOrders;

public interface IGetFinishedProductsByEliteWithoutOrdersUseCase
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(CancellationToken cancellationToken);
}