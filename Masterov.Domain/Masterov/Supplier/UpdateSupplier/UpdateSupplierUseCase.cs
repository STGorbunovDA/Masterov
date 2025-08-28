using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.UpdateSupplier.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.UpdateSupplier;

public class UpdateSupplierUseCase(
    IValidator<UpdateSupplierCommand> validator,
    IUpdateSupplierStorage updateSupplierStorage,
    IGetSupplierByIdStorage getSupplierByIdStorage) : IUpdateSupplierUseCase
{
    public async Task<SupplierDomain> Execute(UpdateSupplierCommand updateSupplierCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateSupplierCommand, cancellationToken);

        var supplierExists =
            await getSupplierByIdStorage.GetSupplierById(updateSupplierCommand.SupplierId, cancellationToken);

        if (supplierExists is null)
            throw new NotFoundByIdException(updateSupplierCommand.SupplierId, "Поставщик");

        return await updateSupplierStorage.UpdateSupplier(updateSupplierCommand.SupplierId, updateSupplierCommand.Name,
            updateSupplierCommand.Address, updateSupplierCommand.Phone, cancellationToken);
    }
}