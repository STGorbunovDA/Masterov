using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.GetProducts;

public interface IGetProductsUseCase
{
    Task<IEnumerable<ProductDomain>> Execute(CancellationToken cancellationToken);
}