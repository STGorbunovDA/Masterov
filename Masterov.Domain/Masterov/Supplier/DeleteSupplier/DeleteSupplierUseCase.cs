using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.DeleteSupplier.Command;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;

namespace Masterov.Domain.Masterov.Supplier.DeleteSupplier;

public class DeleteSupplierUseCase(IValidator<DeleteSupplierCommand> validator, 
    IDeleteSupplierStorage storage, IGetSupplierByIdStorage getSupplierByIdStorage) : IDeleteSupplierUseCase
{
    public async Task<bool> Execute(DeleteSupplierCommand deleteSupplierCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteSupplierCommand, cancellationToken);
        
        var supplierExists = await getSupplierByIdStorage.GetSupplierById(deleteSupplierCommand.SupplierId, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByIdException(deleteSupplierCommand.SupplierId, "Поставщик");
        
        return await storage.DeleteSupplier(deleteSupplierCommand.SupplierId, cancellationToken);
    }
}