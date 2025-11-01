using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSuppliersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliersStorage
{
    public async Task<IEnumerable<SupplierDomain>> GetSuppliers(CancellationToken cancellationToken)
    {
        var suppliers = await dbContext.Suppliers
            .AsNoTracking()
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplierDomain>>(suppliers);
    }
}