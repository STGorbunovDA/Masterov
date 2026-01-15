using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription;

public interface IGetFinishedProductsByDescriptionStorage
{
    Task<IEnumerable<FinishedProductDomain>?> GetFinishedProductsByDescription(string description, CancellationToken cancellationToken);
}