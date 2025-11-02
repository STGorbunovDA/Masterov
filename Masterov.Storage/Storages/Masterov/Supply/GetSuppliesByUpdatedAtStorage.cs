using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

public class GetSuppliesByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByUpdatedAtStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var supplies = await dbContext.Supplies
            .AsNoTracking() 
            .Where(s => s.UpdatedAt >= startOfDay && s.UpdatedAt < endOfDay)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse) 
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}