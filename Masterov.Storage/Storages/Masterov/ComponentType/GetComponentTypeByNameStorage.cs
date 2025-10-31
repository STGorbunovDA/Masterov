using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypeByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypeByNameStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeByName(string productTypeName, CancellationToken cancellationToken) =>
        await dbContext.ComponentTypes
            .AsNoTracking() 
            .Where(f => f.Name.ToLower() == productTypeName.ToLower().Trim())
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}