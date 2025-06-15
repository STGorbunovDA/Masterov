using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;

public interface IGetFinishedProductAtOrderUseCase
{
    Task<FinishedProductDomain?> Execute(GetFinishedProductAtOrderQuery getFinishedProductAtOrderQuery, CancellationToken cancellationToken);
}