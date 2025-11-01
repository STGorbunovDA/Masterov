using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSupplierByIdStorage
{
    public async Task<SupplierDomain?> GetSupplierById(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await dbContext.Suppliers
            .AsNoTracking()
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .FirstOrDefaultAsync(f => f.SupplierId == supplierId, cancellationToken);

        return mapper.Map<SupplierDomain?>(supplier);
    }
}