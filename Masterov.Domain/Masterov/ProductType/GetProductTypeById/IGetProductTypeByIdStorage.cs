using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeById;

public interface IGetProductTypeByIdStorage
{
    Task<ProductTypeDomain?> GetProductTypeById(Guid productTypeId, CancellationToken cancellationToken);
}