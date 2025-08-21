using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByAddressStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSupplierByAddressStorage
{
    public async Task<SupplierDomain?> GetSupplierByAddress(string supplierAddress, CancellationToken cancellationToken)=>
        (await memoryCache.GetOrCreateAsync<SupplierDomain?>( 
            nameof(GetSupplierByAddress),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                
                var supplier = await dbContext.Suppliers
                    .AsNoTracking()
                        .Include(c => c.Supplies)
                            .ThenInclude(p => p.ProductType)
                        .Include(c => c.Supplies)
                            .ThenInclude(p => p.Warehouse)
                        .Where(f => f.Address != null && f.Address.ToLower() == supplierAddress.ToLower().Trim())
                    .FirstOrDefaultAsync(cancellationToken);
                
                return mapper.Map<SupplierDomain>(supplier);
            }))!;
}