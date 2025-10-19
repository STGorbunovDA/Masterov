using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Component.GetComponents;

public class GetComponentsUseCase(IGetComponentsStorage storage) : IGetComponentsUseCase
{
    public async Task<IEnumerable<ComponentsDomain>> Execute(CancellationToken cancellationToken)
    {
        return await storage.GetComponents(cancellationToken);
    }
      
}