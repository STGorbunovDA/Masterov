using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.AddSupplier.Command;
using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.AddSupplier;

public class AddSupplierUseCase(
    IValidator<AddSupplierCommand> validator,
    IAddSupplierStorage addSupplierStorage,
    IGetSupplierByEmailStorage getSupplierByEmailStorage,
    IGetSupplierByPhoneStorage getSupplierByPhoneStorage) : IAddSupplierUseCase
{
    public async Task<SupplierDomain> Execute(AddSupplierCommand addSupplierCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addSupplierCommand, cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(addSupplierCommand.Email))
        {
            var supplierByEmail = await getSupplierByEmailStorage.GetSupplierByEmail(
                addSupplierCommand.Email, cancellationToken);
            if (supplierByEmail is not null)
                throw new SupplierExistsException(
                    addSupplierCommand.Name, 
                    addSupplierCommand.Surname,
                    addSupplierCommand.Email,
                    addSupplierCommand.Phone,
                    addSupplierCommand.Address);
        }
    
        if (!string.IsNullOrWhiteSpace(addSupplierCommand.Phone))
        {
            var supplierByPhone = await getSupplierByPhoneStorage.GetSupplierByPhone(
                addSupplierCommand.Phone, cancellationToken);
            if (supplierByPhone is not null)
                throw new SupplierExistsException(
                    addSupplierCommand.Name, 
                    addSupplierCommand.Surname,
                    addSupplierCommand.Email,
                    addSupplierCommand.Phone,
                    addSupplierCommand.Address);
        }

        return await addSupplierStorage.AddSupplier(addSupplierCommand.Name, addSupplierCommand.Surname, addSupplierCommand.Email,
            addSupplierCommand.Phone, addSupplierCommand.Address, cancellationToken);
    }
}