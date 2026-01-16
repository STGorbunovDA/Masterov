using Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt;

public interface IGetProductTypesByCreatedAtUseCase
{
    Task<IEnumerable<ProductTypeDomain>?> Execute(GetProductTypesByCreatedAtQuery query, CancellationToken cancellationToken);
}