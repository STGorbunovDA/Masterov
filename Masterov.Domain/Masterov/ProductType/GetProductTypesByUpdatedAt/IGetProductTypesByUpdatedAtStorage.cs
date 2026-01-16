using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt;

public interface IGetProductTypesByUpdatedAtStorage
{
    Task<IEnumerable<ProductTypeDomain>?> GetProductTypesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}