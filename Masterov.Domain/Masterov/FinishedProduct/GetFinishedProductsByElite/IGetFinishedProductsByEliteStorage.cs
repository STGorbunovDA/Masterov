using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByElite;

public interface IGetFinishedProductsByEliteStorage
{
    Task<IEnumerable<FinishedProductDomain?>> GetFinishedProductsByElite(CancellationToken cancellationToken);
}