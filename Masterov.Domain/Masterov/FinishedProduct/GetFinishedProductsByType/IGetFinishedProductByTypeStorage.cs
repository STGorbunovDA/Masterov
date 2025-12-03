using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType;

public interface IGetFinishedProductByTypeStorage
{
    Task<IEnumerable<FinishedProductDomain?>> GetFinishedProductByType(string finishedProductType, CancellationToken cancellationToken);
}