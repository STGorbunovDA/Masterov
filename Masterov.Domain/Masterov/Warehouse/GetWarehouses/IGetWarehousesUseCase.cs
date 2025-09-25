using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouses;

public interface IGetWarehousesUseCase
{
    Task<IEnumerable<WarehouseDomain>> Execute(CancellationToken cancellationToken);
}