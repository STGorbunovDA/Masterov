using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypes;

public interface IGetProductsTypeStorage
{
    Task<IEnumerable<ComponentTypeDomain>> GetComponentTypes(CancellationToken cancellationToken);
}