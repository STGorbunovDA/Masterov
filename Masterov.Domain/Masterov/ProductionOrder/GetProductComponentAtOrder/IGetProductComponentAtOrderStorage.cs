using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;

public interface IGetProductComponentAtOrderStorage
{
    Task<IEnumerable<ProductComponentDomain?>> GetProductComponentAtOrder(Guid orderId, CancellationToken cancellationToken);
}