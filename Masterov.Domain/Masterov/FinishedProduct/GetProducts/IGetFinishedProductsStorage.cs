using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetProducts;

public interface IGetFinishedProductsStorage
{
    Task<IEnumerable<FinishedProductDomain>> GetFinishedProducts(CancellationToken cancellationToken);
}