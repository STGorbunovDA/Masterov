using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt;

public interface IGetFinishedProductsByCreatedAtUseCase
{
    Task<IEnumerable<FinishedProductDomain>?> Execute(GetFinishedProductsByCreatedAtQuery getFinishedProductsByCreatedAtQuery, CancellationToken cancellationToken);
}