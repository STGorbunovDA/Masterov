using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType;

public interface IGetFinishedProductsByTypeUseCase
{
    Task<IEnumerable<FinishedProductDomain?>> Execute(GetFinishedProductsByTypeQuery finishedProductsByTypeQuery, CancellationToken cancellationToken);
}