using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class DeleteUsedComponentStorage(MasterovDbContext dbContext) : IDeleteUsedComponentStorage
{
    public async Task<bool> DeleteUsedComponent(Guid usedComponentId, CancellationToken cancellationToken)
    {
        var usedComponent = await dbContext.Set<Storage.UsedComponent>().FindAsync(new object[] { usedComponentId }, cancellationToken);
        
        if (usedComponent == null)
            return false;
        
        dbContext.Set<Storage.UsedComponent>().Remove(usedComponent);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}