using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;

public interface IGetFinishedProductByIdWithoutOrdersUseCase
{
    Task<FinishedProductWithoutOrdersDomain?> Execute(GetFinishedProductByIdWithoutOrdersQuery getFinishedProductByIdQuery, CancellationToken cancellationToken);
}