using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSupplierByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSupplierByNameStorage
{
    public async Task<SupplierDomain?> GetSupplierByName(string supplierName, CancellationToken cancellationToken)=>
        (await memoryCache.GetOrCreateAsync<SupplierDomain?>( 
            nameof(GetSupplierByName),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                
                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                        .Include(c => c.Supplies)
                    .Where(f => f.Name.ToLower() == supplierName.ToLower().Trim())
                    .FirstOrDefaultAsync( cancellationToken);
                
                return mapper.Map<SupplierDomain>(supplier);
            }))!;
}