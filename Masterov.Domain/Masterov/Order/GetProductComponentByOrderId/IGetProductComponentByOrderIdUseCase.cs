using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public interface IGetProductComponentByOrderIdUseCase
{
    Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentByOrderIdQuery getProductComponentByOrderIdQuery, CancellationToken cancellationToken);
}