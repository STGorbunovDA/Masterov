using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public interface IGetComponentsByOrderIdUseCase
{
    Task<IEnumerable<ComponentsDomain?>> Execute(GetComponentsByOrderIdQuery getComponentsByOrderIdQuery, CancellationToken cancellationToken);
}