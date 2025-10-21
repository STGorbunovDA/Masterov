using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;

public interface IGetOrderByUsedComponentIdUseCase
{
    Task<OrderDomain?> Execute(GetOrderByUsedComponentIdQuery orderByUsedComponentIdQuery, CancellationToken cancellationToken);
}