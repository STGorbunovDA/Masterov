using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;

public interface IGetOrdersByFinishedProductUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetOrdersByFinishedProductQuery getOrdersByFinishedProductQuery, CancellationToken cancellationToken);
}