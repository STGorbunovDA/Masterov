using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;

public interface IAddUsedComponentStorage
{
    Task<UsedComponentDomain> AddUsedComponent(Guid orderId, Guid componentTypeId, Guid warehouseId, int quantity, CancellationToken cancellationToken);
}