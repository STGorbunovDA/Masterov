using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescriptionAtWithoutOrders;

public interface IGetFinishedProductsByDescriptionAtWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain>?> GetFinishedProductsByDescriptionAtWithoutOrders(string description, CancellationToken cancellationToken);
}