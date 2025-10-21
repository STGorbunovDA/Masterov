using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;

public interface IGetUsedComponentByIdUseCase
{
    Task<UsedComponentDomain?> Execute(GetUsedComponentByIdQuery usedComponentByIdQuery, CancellationToken cancellationToken);
}