using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByElite;

public interface IGetFinishedProductsByEliteUseCase
{
    Task<IEnumerable<FinishedProductDomain?>> Execute(CancellationToken cancellationToken);
}