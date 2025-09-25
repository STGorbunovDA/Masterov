using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouses;

public class GetWarehousesUseCase(IGetWarehousesStorage storage) : IGetWarehousesUseCase
{
    public async Task<IEnumerable<WarehouseDomain>> Execute(CancellationToken cancellationToken)
    {
        return await storage.GetWarehouses(cancellationToken);
    }
}