using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;

public interface IGetFinishedProductByNameUseCase
{
    Task<FinishedProductDomain?> Execute(GetFinishedProductByNameQuery finishedProductByNameQuery, CancellationToken cancellationToken);
}