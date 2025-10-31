using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypeByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypeByIdStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeById(Guid componentTypeId, CancellationToken cancellationToken) =>
        await dbContext.ComponentTypes
            .AsNoTracking() 
            .Where(f => f.ComponentTypeId == componentTypeId)
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}