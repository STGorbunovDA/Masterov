using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductsType;

public class GetProductsTypeUseCase(IGetProductsTypeStorage storage) : IGetProductsTypeUseCase
{
    public async Task<IEnumerable<ProductTypeDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetProductsType(cancellationToken);
}