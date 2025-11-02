using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

public class GetSuppliersByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliersByUpdatedAtStorage
{
    public async Task<IEnumerable<SupplierDomain>?> GetSuppliersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var supplier = await dbContext.Suppliers
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplierDomain>>(supplier);
    }
}