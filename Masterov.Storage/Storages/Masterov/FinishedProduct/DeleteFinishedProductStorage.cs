using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class DeleteFinishedProductStorage(MasterovDbContext dbContext) : IDeleteFinishedProductStorage
{
    public async Task<bool> DeleteFinishedProduct(Guid finishedProductId, CancellationToken cancellationToken)
    {
        var finishedProduct = await dbContext.Set<Storage.FinishedProduct>().FindAsync(new object[] { finishedProductId }, cancellationToken);
        
        if (finishedProduct == null)
            return false;
        
        dbContext.Set<Storage.FinishedProduct>().Remove(finishedProduct);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}