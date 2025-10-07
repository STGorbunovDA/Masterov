using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId;

public interface IGetFinishedProductByOrderIdUseCase
{
    Task<FinishedProductDomain?> Execute(GetFinishedProductByOrderIdQuery getFinishedProductByOrderIdQuery, CancellationToken cancellationToken);
}