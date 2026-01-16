using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt;

public interface IGetProductTypesByCreatedAtStorage
{
    Task<IEnumerable<ProductTypeDomain>?> GetProductTypesByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}