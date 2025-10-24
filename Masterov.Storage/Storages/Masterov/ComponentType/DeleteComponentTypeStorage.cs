using Masterov.Domain.Masterov.ComponentType.DeleteComponentType;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class DeleteComponentTypeStorage(MasterovDbContext dbContext) : IDeleteComponentTypeStorage
{
    public async Task<bool> DeleteComponentType(Guid productTypeId, CancellationToken cancellationToken)
    {
        var productType = await dbContext.Set<Storage.ComponentType>().FindAsync(new object[] { productTypeId }, cancellationToken);
        
        if (productType == null)
            return false;
        
        dbContext.Set<Storage.ComponentType>().Remove(productType);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}