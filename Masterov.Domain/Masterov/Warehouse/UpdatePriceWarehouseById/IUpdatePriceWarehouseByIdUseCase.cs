using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;

public interface IUpdatePriceWarehouseByIdUseCase
{
    Task<WarehouseDomain> Execute(UpdatePriceWarehouseByIdCommand command, CancellationToken cancellationToken);
}