using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSuppliesBySupplierIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesBySupplierIdStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await dbContext.Suppliers
            .AsNoTracking()
                .Include(s => s.Supplies)
                    .ThenInclude(o => o.Warehouse)
                .Include(s => s.Supplies)
                    .ThenInclude(o => o.ComponentType)
            .FirstOrDefaultAsync(c => c.SupplierId == supplierId, cancellationToken);

        return supplier == null ? null : mapper.Map<IEnumerable<SupplyDomain>>(supplier.Supplies);
    }
}