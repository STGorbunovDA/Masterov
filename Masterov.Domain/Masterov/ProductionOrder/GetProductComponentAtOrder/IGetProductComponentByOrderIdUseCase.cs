using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;

public interface IGetProductComponentByOrderIdUseCase
{
    Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentByOrderIdQuery getProductComponentByOrderIdQuery, CancellationToken cancellationToken);
}