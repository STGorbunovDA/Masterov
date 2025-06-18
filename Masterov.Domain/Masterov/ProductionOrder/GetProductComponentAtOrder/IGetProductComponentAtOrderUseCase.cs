using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;

public interface IGetProductComponentAtOrderUseCase
{
    Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentAtOrderQuery getProductComponentAtOrderQuery, CancellationToken cancellationToken);
}