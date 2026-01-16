using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypes;

public class GetProductTypesUseCase(IGetProductTypesStorage storage) : IGetProductTypesUseCase
{
    public async Task<IEnumerable<ProductTypeDomain?>> Execute(CancellationToken cancellationToken) =>
        await storage.GetProductTypes(cancellationToken);
}