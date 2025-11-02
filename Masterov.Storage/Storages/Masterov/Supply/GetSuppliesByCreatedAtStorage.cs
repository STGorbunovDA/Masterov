using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

public class GetSuppliesByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByCreatedAtStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var supplies = await dbContext.Supplies
            .AsNoTracking() 
            .Where(s => s.CreatedAt >= startOfDay && s.CreatedAt < endOfDay)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse) 
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}