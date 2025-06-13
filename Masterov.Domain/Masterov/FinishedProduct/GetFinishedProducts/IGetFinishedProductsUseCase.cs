using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;

public interface IGetFinishedProductsUseCase
{
    Task<IEnumerable<FinishedProductDomain>> Execute(CancellationToken cancellationToken);
}