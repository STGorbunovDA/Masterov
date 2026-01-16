using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypes;

public interface IGetProductTypesUseCase
{
    Task<IEnumerable<ProductTypeDomain?>> Execute(CancellationToken cancellationToken);
}