using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSupplyByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSupplyByIdStorage
{
    public async Task<SupplyDomain?> GetSupplyById(Guid supplyId, CancellationToken cancellationToken)
    {
        var supply = await dbContext.Supplies
            .AsNoTracking()
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse)
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .FirstOrDefaultAsync(f => f.SupplyId == supplyId, cancellationToken);

        return mapper.Map<SupplyDomain?>(supply);
    }
}