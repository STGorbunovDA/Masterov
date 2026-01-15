using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription;

public interface IGetFinishedProductsByDescriptionUseCase
{
    Task<IEnumerable<FinishedProductDomain>?> Execute(GetFinishedProductsByDescriptionQuery getFinishedProductsByDescriptionQuery, CancellationToken cancellationToken);
}