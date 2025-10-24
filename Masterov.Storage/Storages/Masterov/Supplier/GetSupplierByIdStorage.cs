using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSupplierByIdStorage
{
    public async Task<SupplierDomain?> GetSupplierById(Guid supplierId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<SupplierDomain?>( 
            nameof(GetSupplierById),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                        .Include(c => c.Supplies)
                            .ThenInclude(p => p.ComponentType)
                        .Include(c => c.Supplies)
                            .ThenInclude(p => p.Warehouse)
                    .Where(f => f.SupplierId == supplierId)
                    .FirstOrDefaultAsync( cancellationToken);
                
                return mapper.Map<SupplierDomain>(supplier);
            }))!;
}