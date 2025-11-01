using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSupplierByNameStorage
{
    public async Task<IEnumerable<SupplierDomain?>> GetSupplierByName(string supplierName, CancellationToken cancellationToken)
    {
        var normalizedName = supplierName.Trim().ToLower();

        var suppliers = await dbContext.Suppliers
            .AsNoTracking()
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .Where(f => f.Name.ToLower().Contains(normalizedName))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplierDomain>>(suppliers);
    }
}