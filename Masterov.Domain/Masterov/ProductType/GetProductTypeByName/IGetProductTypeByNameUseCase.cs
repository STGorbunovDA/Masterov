using Masterov.Domain.Masterov.ProductType.GetProductTypeByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeByName;

public interface IGetProductTypeByNameUseCase
{
    Task<ProductTypeDomain?> Execute(GetProductTypeByNameQuery getProductTypeByNameQuery, CancellationToken cancellationToken);
}