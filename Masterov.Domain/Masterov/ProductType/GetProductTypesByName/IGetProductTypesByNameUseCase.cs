using Masterov.Domain.Masterov.ProductType.GetProductTypesByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByName;

public interface IGetProductTypesByNameUseCase
{
    Task<IEnumerable<ProductTypeDomain?>> Execute(GetProductTypesByNameQuery typesByNameQuery, CancellationToken cancellationToken);
}