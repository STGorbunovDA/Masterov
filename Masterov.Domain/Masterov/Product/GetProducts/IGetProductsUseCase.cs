using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Product.GetProducts;

public interface IGetProductsUseCase
{
    Task<IEnumerable<ProductDomain>> Execute(CancellationToken cancellationToken);
}