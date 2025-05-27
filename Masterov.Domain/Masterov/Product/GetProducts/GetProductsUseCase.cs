using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Product.GetProducts;

public class GetProductsUseCase(IGetProductsStorage storage) : IGetProductsUseCase
{
    public async Task<IEnumerable<ProductDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetProducts(cancellationToken);
}