using Masterov.Domain.Masterov.ProductType.DeleteProductType;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class DeleteProductTypeStorage(MasterovDbContext dbContext) : IDeleteProductTypeStorage
{
    public async Task<bool> DeleteProductType(Guid productTypeId, CancellationToken cancellationToken)
    {
        var productType = await dbContext.Set<Storage.ProductType>().FindAsync(new object[] { productTypeId }, cancellationToken);
        
        if (productType == null)
            return false;
        
        dbContext.Set<Storage.ProductType>().Remove(productType);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}