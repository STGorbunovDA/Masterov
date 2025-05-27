using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductsType;

public interface IGetProductsTypeUseCase
{
    Task<IEnumerable<ProductTypeDomain>> Execute(CancellationToken cancellationToken);
}