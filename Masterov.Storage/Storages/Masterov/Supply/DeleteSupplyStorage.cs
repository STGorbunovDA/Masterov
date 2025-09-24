using Masterov.Domain.Masterov.Supply.DeleteSupply;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class DeleteSupplyStorage(MasterovDbContext dbContext) : IDeleteSupplyStorage
{
    public async Task<bool> DeleteSupply(Guid supplyId, CancellationToken cancellationToken)
    {
        var supply = await dbContext.Set<Storage.Supply>().FindAsync(new object[] { supplyId }, cancellationToken);
        
        if (supply == null)
            return false;
        
        dbContext.Set<Storage.Supply>().Remove(supply);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}