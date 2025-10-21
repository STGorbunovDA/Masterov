using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;

public interface IGetUsedComponentsByOrderIdUseCase
{
    Task<IEnumerable<UsedComponentDomain?>> Execute(GetUsedComponentsByOrderIdQuery getUsedComponentsByOrderIdQuery, CancellationToken cancellationToken);
}