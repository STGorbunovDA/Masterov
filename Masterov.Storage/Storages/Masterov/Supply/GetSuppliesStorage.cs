using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSupplies;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSuppliesStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesStorage
{
    public async Task<IEnumerable<SupplyDomain?>> GetSupplies(CancellationToken cancellationToken)
    {
        var supplies = await dbContext.Supplies
            .AsNoTracking()
                .Include(c => c.Supplier)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse)
                    .ThenInclude(w => w.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}