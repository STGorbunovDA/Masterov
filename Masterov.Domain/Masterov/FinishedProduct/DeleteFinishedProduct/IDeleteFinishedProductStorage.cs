namespace Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;

public interface IDeleteFinishedProductStorage
{
    Task<bool> DeleteFinishedProduct(Guid finishedProductId, CancellationToken cancellationToken);
}