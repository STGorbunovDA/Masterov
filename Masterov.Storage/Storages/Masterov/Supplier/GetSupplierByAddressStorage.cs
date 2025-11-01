using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByAddressStorage(MasterovDbContext dbContext, IMapper mapper) 
    : IGetSupplierByAddressStorage
{
    public async Task<IEnumerable<SupplierDomain?>> GetSuppliersByAddress(string supplierAddress, CancellationToken cancellationToken)
    {
        var normalizedAddress = supplierAddress.Trim().ToLower();

        var suppliers = await dbContext.Suppliers
            .AsNoTracking()
            .Include(c => c.Supplies)
            .ThenInclude(p => p.ComponentType)
            .Include(c => c.Supplies)
            .ThenInclude(p => p.Warehouse)
            .Where(f => f.Address.ToLower().Contains(normalizedAddress))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplierDomain>>(suppliers);
    }
}