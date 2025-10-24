using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class UpdateComponentTypeStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateProductTypeStorage
{
    public async Task<ComponentTypeDomain> UpdateComponentType(Guid productTypeId, string name, string? description, CancellationToken cancellationToken)
    {
        var productType = await dbContext.Set<Storage.ComponentType>().FindAsync([productTypeId], cancellationToken);
        
        productType!.Name = name; 
        productType.Description = description;

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ComponentTypeDomain>(productType);
    }
}