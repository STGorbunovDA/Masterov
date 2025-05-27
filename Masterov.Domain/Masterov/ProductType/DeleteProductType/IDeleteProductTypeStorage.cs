namespace Masterov.Domain.Masterov.ProductType.DeleteProductType;

public interface IDeleteProductTypeStorage
{
    Task<bool> DeleteProductType(Guid productTypeId, CancellationToken cancellationToken);
}