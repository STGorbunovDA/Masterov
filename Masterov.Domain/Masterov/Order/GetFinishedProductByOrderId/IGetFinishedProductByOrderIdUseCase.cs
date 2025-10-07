using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;

public interface IGetFinishedProductByOrderIdUseCase
{
    Task<FinishedProductDomain?> Execute(GetFinishedProductByOrderIdQuery getFinishedProductByOrderIdQuery, CancellationToken cancellationToken);
}