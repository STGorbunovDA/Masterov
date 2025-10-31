using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class UpdateComponentTypeStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateComponentTypeStorage
{
    public async Task<ComponentTypeDomain> UpdateComponentType(Guid componentTypeId, string name, DateTime? createdAt,
        string? description, CancellationToken cancellationToken)
    {
        var componentType = await dbContext.Set<Storage.ComponentType>().FindAsync([componentTypeId], cancellationToken);

        if (componentType == null)
            throw new Exception("componentType not found");
        
        componentType.Name = name;
        componentType.Description = description;
        
        if (createdAt.HasValue)
            componentType.CreatedAt = createdAt.Value;
        
        componentType.UpdatedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<ComponentTypeDomain>(componentType);
    }
}