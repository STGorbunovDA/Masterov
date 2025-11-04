using Masterov.Domain.Masterov.Warehouse.DeleteWarehouse.Command;

namespace Masterov.Domain.Masterov.Warehouse.DeleteWarehouse;

public interface IDeleteWarehouseUseCase
{
    Task<bool> Execute(DeleteWarehouseCommand deleteWarehouseCommand, CancellationToken cancellationToken);
}