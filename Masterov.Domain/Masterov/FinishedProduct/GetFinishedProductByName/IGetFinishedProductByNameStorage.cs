using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;

public interface IGetFinishedProductByNameStorage
{
    Task<IEnumerable<FinishedProductDomain?>> GetFinishedProductByName(string finishedProductName, CancellationToken cancellationToken);
}