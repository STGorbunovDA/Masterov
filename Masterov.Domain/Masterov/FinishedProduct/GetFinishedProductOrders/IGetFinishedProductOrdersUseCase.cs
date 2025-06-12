using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;

public interface IGetFinishedProductOrdersUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetFinishedProductOrdersQuery getFinishedProductOrdersQuery, CancellationToken cancellationToken);
}