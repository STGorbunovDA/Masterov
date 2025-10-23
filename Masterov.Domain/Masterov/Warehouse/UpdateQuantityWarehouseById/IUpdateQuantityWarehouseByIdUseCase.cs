using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;

public interface IUpdateQuantityWarehouseByIdUseCase
{
    Task<WarehouseDomain> Execute(UpdateQuantityWarehouseByIdCommand command, CancellationToken cancellationToken);
}