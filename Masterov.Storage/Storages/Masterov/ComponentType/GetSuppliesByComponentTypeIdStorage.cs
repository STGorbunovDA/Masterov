using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

public class GetSuppliesByComponentTypeIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByComponentTypeIdStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesByComponentTypeId(Guid componentTypeId, CancellationToken cancellationToken)
    {
        var supplies = await dbContext.Supplies
            .AsNoTracking()
            .Where(u => u.ComponentTypeId == componentTypeId)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse) 
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}