using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

public class GetComponentTypesByDescriptionStorage (MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypesByDescriptionStorage
{
    public async Task<IEnumerable<ComponentTypeDomain>?> GetComponentTypesByDescription(string description, CancellationToken cancellationToken)
    {
        var orders = await dbContext.ComponentTypes
            .AsNoTracking() 
            .Where(order => order.Description != null && order.Description.Contains(description))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ComponentTypeDomain>>(orders);
    }
}