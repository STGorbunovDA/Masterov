using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

public class GetNewSuppliesBySupplierIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetNewSuppliesBySupplierIdStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetNewSuppliesBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await dbContext.Suppliers
            .AsNoTracking()
                .Include(s => s.Supplies)
                    .ThenInclude(o => o.Warehouse)
                .Include(s => s.Supplies)
                    .ThenInclude(o => o.ProductType)
            .FirstOrDefaultAsync(c => c.SupplierId == supplierId, cancellationToken);

        return supplier == null ? null : mapper.Map<IEnumerable<SupplyDomain>>(supplier.Supplies);
    }
}