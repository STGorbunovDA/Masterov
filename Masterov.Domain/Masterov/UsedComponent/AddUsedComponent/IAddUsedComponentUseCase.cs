using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;

public interface IAddUsedComponentUseCase
{
    Task<UsedComponentDomain> Execute(AddUsedComponentCommand addUsedComponentCommand, CancellationToken cancellationToken);
}