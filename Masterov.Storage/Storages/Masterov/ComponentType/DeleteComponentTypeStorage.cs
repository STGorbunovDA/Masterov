using Masterov.Domain.Masterov.ComponentType.DeleteComponentType;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class DeleteComponentTypeStorage(MasterovDbContext dbContext) : IDeleteComponentTypeStorage
{
    public async Task<bool> DeleteComponentType(Guid componentTypeId, CancellationToken cancellationToken)
    {
        var componentType = await dbContext.Set<Storage.ComponentType>().FindAsync(new object[] { componentTypeId }, cancellationToken);
        
        if (componentType == null)
            return false;
        
        dbContext.Set<Storage.ComponentType>().Remove(componentType);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}