using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouses;

public interface IGetWarehousesStorage
{
    Task<IEnumerable<WarehouseDomain>> GetWarehouses(CancellationToken cancellationToken);
}