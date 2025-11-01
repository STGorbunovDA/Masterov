using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;

public interface IGetFinishedProductsStorage
{
    Task<IEnumerable<FinishedProductDomain?>> GetFinishedProducts(CancellationToken cancellationToken);
}