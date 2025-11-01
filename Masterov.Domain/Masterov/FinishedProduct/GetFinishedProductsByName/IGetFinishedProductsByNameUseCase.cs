using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName;

public interface IGetFinishedProductsByNameUseCase
{
    Task<IEnumerable<FinishedProductDomain?>> Execute(GetFinishedProductsByNameQuery finishedProductsByNameQuery, CancellationToken cancellationToken);
}