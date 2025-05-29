using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.UpdateProductType;

public interface IUpdateProductTypeStorage
{
    Task<ProductTypeDomain> UpdateProductType(Guid productTypeId, string name, string? description, CancellationToken cancellationToken);
}