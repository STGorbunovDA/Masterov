using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypes;

public class ComponentTypesUseCase(IGetComponentTypesStorage storage) : IGetComponentTypesUseCase
{
    public async Task<IEnumerable<ComponentTypeDomain?>> Execute(CancellationToken cancellationToken) =>
        await storage.GetComponentTypes(cancellationToken);
}