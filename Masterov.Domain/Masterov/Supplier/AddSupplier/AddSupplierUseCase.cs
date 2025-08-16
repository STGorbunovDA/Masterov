using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.AddSupplier.Command;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.AddSupplier;

public class AddSupplierUseCase(
    IValidator<AddSupplierCommand> validator,
    IAddSupplierStorage addSupplierStorage,
    IGetSupplierByAddressStorage getSupplierByAddressStorage,
    IGetSupplierByPhoneStorage getSupplierByPhoneStorage) : IAddSupplierUseCase
{
    public async Task<SupplierDomain> Execute(AddSupplierCommand addSupplierCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addSupplierCommand, cancellationToken);
        
        SupplierDomain? supplier = null;

        if (addSupplierCommand.Phone is not null)
            supplier = await getSupplierByPhoneStorage.GetSupplierByPhone(addSupplierCommand.Phone, cancellationToken);
        else if (supplier is null && addSupplierCommand.Address is not null)
            supplier = await getSupplierByAddressStorage.GetSupplierByAddress(addSupplierCommand.Address, cancellationToken);

        if (supplier is not null)
            if (supplier is { Address: not null, Phone: not null })
                throw new SupplierExistsException(supplier.Name, supplier.Address, supplier.Phone);

        return await addSupplierStorage.AddSupplier(addSupplierCommand.Name,
            addSupplierCommand?.Address, addSupplierCommand?.Phone, cancellationToken);
    }
}