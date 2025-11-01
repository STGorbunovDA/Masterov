using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypes;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypesStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypesStorage
{
    public async Task<IEnumerable<ComponentTypeDomain?>> GetComponentTypes(CancellationToken cancellationToken)
    {
        return await dbContext.ComponentTypes
            .AsNoTracking()
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}