using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByName;

public interface IGetProductTypesByNameStorage
{
    Task<IEnumerable<ProductTypeDomain?>> GetProductTypesByName(string productTypeName, CancellationToken cancellationToken);
}