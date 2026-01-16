using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypes;

public interface IGetProductTypesStorage
{
    Task<IEnumerable<ProductTypeDomain?>> GetProductTypes(CancellationToken cancellationToken);
}