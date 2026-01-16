using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeById;

public interface IGetProductTypeByIdUseCase
{
    Task<ProductTypeDomain?> Execute(GetProductTypeByIdQuery productTypeByIdQuery, CancellationToken cancellationToken);
}