using Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt;

public interface IGetProductTypesByUpdatedAtUseCase
{
    Task<IEnumerable<ProductTypeDomain>?> Execute(GetProductTypesByUpdatedAtQuery query, CancellationToken cancellationToken);
}