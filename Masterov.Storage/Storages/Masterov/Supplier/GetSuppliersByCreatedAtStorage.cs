using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

public class GetSuppliersByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliersByCreatedAtStorage
{
    public async Task<IEnumerable<SupplierDomain>?> GetSuppliersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var supplier = await dbContext.Suppliers
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplierDomain>>(supplier);
    }
}