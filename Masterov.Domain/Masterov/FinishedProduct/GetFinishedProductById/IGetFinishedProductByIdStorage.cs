using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;

public interface IGetFinishedProductByIdStorage
{
    Task<FinishedProductDomain?> GetFinishedProductById(Guid finishedProductId, CancellationToken cancellationToken);
}