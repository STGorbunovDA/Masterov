using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetProducts;

public interface IGetFinishedProductsUseCase
{
    Task<IEnumerable<FinishedProductDomain>> Execute(CancellationToken cancellationToken);
}