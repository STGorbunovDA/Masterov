using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSuppliersStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSuppliersStorage
{
    public async Task<IEnumerable<SupplierDomain>> GetSuppliers(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(
            nameof(GetSuppliers),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var customers = await dbContext.Suppliers
                    .AsNoTracking()
                        .Include(c => c.Supplies)
                    .ToArrayAsync(cancellationToken);

                return mapper.Map<SupplierDomain[]>(customers); 
            }))!;
}