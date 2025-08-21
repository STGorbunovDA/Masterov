using Masterov.Domain.Masterov.Supplier.DeleteSupplier.Command;

namespace Masterov.Domain.Masterov.Supplier.DeleteSupplier;

public interface IDeleteSupplierUseCase
{
    Task<bool> Execute(DeleteSupplierCommand deleteSupplierCommand, CancellationToken cancellationToken);
}