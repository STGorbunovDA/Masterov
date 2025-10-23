using Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId;

public interface IGetProductTypeByUsedComponentIdUseCase
{
    Task<ProductTypeDomain?> Execute(GetProductTypeByUsedComponentIdQuery productTypeByUsedComponentIdQuery, CancellationToken cancellationToken);
}