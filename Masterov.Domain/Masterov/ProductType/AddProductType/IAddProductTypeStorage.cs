using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.AddProductType;

public interface IAddProductTypeStorage
{
    Task<ProductTypeDomain> AddProductType(string name, CancellationToken cancellationToken);
}