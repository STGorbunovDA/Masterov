using Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent;

public interface IUpdateUsedComponentUseCase
{
    Task<UsedComponentDomain> Execute(UpdateUsedComponentCommand command, CancellationToken cancellationToken);
}