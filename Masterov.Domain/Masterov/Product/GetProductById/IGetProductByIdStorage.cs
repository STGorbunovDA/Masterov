using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Product.GetProductById;

public interface IGetProductByIdStorage
{
    Task<ProductDomain?> GetProductById(Guid productId, CancellationToken cancellationToken);
}