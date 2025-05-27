using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductsType;

public interface IGetProductsTypeStorage
{
    Task<IEnumerable<ProductTypeDomain>> GetProductsType(CancellationToken cancellationToken);
}