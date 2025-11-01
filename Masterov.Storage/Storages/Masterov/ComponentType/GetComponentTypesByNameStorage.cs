using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypesByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypeByNameStorage
{
    public async Task<IEnumerable<ComponentTypeDomain?>> GetComponentTypesByName(string componentTypeName, CancellationToken cancellationToken)
    {
        var normalizedName = componentTypeName.Trim().ToLower();

        return await dbContext.ComponentTypes
            .AsNoTracking()
            .Where(f => f.Name.ToLower().Contains(normalizedName))
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}