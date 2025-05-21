using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.GetProducts;

public interface IGetProductsStorage
{
    Task<IEnumerable<ProductDomain>> GetProducts(CancellationToken cancellationToken);
}