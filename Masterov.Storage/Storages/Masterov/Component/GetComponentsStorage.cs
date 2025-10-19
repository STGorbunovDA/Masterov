using AutoMapper;
using Masterov.Domain.Masterov.Component.GetComponents;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Component;

internal class GetComponentsStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentsStorage
{
    public async Task<IEnumerable<ComponentsDomain>> GetComponents(CancellationToken cancellationToken)
    {
        var customers = await dbContext.Components
            .AsNoTracking()
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ProductType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ComponentsDomain>>(customers);
    }
}