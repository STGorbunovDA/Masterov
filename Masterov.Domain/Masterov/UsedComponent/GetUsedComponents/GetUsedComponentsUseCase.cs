using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponents;

public class GetUsedComponentsUseCase(IGetUsedComponentsStorage storage) : IGetUsedComponentsUseCase
{
    public async Task<IEnumerable<UsedComponentDomain?>> Execute(CancellationToken cancellationToken)
    {
        return await storage.GetUsedComponents(cancellationToken);
    }
      
}