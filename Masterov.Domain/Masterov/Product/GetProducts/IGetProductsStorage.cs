using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Product.GetProducts;

public interface IGetProductsStorage
{
    Task<IEnumerable<ProductDomain>> GetProducts(CancellationToken cancellationToken);
}