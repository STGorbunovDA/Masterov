namespace Masterov.Domain.Masterov.Supplier.DeleteSupplier;

public interface IDeleteSupplierStorage
{
    Task<bool> DeleteSupplier(Guid supplierId, CancellationToken cancellationToken);
}