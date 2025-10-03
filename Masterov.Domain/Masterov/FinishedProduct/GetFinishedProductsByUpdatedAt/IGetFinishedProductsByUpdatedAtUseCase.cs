using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt;

public interface IGetFinishedProductsByUpdatedAtUseCase
{
    Task<IEnumerable<FinishedProductDomain>?> Execute(GetFinishedProductsByUpdatedAtQuery getFinishedProductsByUpdatedAtQuery, CancellationToken cancellationToken);
}