using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetComponentTypeBySupplyIdStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetComponentTypeBySupplyIdStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeBySupplyId(Guid supplyId, CancellationToken cancellationToken) =>
        await dbContext.Supplies
            .AsNoTracking()
            .Where(o => o.SupplyId == supplyId)
            .Select(o => o.ComponentType)
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}