using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders;

public interface IGetFinishedProductsByTypeWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductByTypeWithoutOrders(string finishedProductType, CancellationToken cancellationToken);
}