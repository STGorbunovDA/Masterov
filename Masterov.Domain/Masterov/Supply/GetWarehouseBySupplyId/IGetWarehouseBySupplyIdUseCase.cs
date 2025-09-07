using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;

public interface IGetWarehouseBySupplyIdUseCase
{
    Task<WarehouseDomain?> Execute(GetWarehouseBySupplyIdQuery getWarehouseBySupplyIdQuery, CancellationToken cancellationToken);
}