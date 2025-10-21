using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponents;

public class UsedUsedComponentsUseCase(IGetUsedComponentsStorage storage) : IGetUsedComponentsUseCase
{
    public async Task<IEnumerable<UsedComponentDomain>> Execute(CancellationToken cancellationToken)
    {
        return await storage.GetUsedComponents(cancellationToken);
    }
      
}