using Masterov.Domain.Masterov.Supplier.DeleteSupplier;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class DeleteSupplierStorage(MasterovDbContext dbContext) : IDeleteSupplierStorage
{
    public async Task<bool> DeleteSupplier(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await dbContext.Set<Storage.Supplier>().FindAsync(new object[] { supplierId }, cancellationToken);
        
        if (supplier == null)
            return false;
        
        dbContext.Set<Storage.Supplier>().Remove(supplier);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}