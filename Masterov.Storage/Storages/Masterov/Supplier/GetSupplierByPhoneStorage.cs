using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByPhoneStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSupplierByPhoneStorage
{
    public async Task<SupplierDomain?> GetSupplierByPhone(string supplierPhone, CancellationToken cancellationToken)=>
        (await memoryCache.GetOrCreateAsync<SupplierDomain?>( 
            nameof(GetSupplierByPhone),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                
                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                        .Include(c => c.Supplies)
                            .ThenInclude(p => p.ComponentType)
                        .Include(c => c.Supplies)
                            .ThenInclude(p => p.Warehouse)
                        .Where(f => f.Phone != null && f.Phone.ToLower() == supplierPhone.ToLower().Trim())
                    .FirstOrDefaultAsync(cancellationToken);
                
                return mapper.Map<SupplierDomain>(supplier);
            }))!;
}