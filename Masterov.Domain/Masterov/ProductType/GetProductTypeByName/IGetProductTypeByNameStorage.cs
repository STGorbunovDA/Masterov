using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeByName;

public interface IGetProductTypeByNameStorage
{
    Task<ProductTypeDomain?> GetProductTypeByName(string productTypeName, CancellationToken cancellationToken);
}