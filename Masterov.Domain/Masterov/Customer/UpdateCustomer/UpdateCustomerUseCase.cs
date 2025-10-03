using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.UpdateCustomer;

public class UpdateCustomerUseCase(IValidator<UpdateCustomerCommand> validator,
    IUpdateCustomerStorage updateCustomerStorage, 
    IGetCustomerByIdStorage getCustomerByIdStorage, 
    IGetCustomerByPhoneStorage getCustomerByPhoneStorage,
    IGetCustomerByEmailStorage getCustomerByEmailStorage) : IUpdateCustomerUseCase
{
    public async Task<CustomerDomain> Execute(UpdateCustomerCommand updateCustomerCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateCustomerCommand, cancellationToken);
        
        var customerExists = await getCustomerByIdStorage.GetCustomerById(updateCustomerCommand.CustomerId, cancellationToken);

        if (customerExists is null)
            throw new NotFoundByIdException(updateCustomerCommand.CustomerId, "Заказчик");
        
        if (!string.IsNullOrWhiteSpace(updateCustomerCommand.Email))
        {
            var existingByEmail = await getCustomerByEmailStorage.GetCustomerByEmail(updateCustomerCommand.Email, cancellationToken);
            if (existingByEmail != null && existingByEmail.CustomerId != updateCustomerCommand.CustomerId)
                throw new Conflict409Exception($"Заказчик с email '{updateCustomerCommand.Email}' уже существует.");
        }
        
        if (!string.IsNullOrWhiteSpace(updateCustomerCommand.Phone))
        {
            var existingByPhone = await getCustomerByPhoneStorage.GetCustomerByPhone(updateCustomerCommand.Phone, cancellationToken);
            if (existingByPhone != null && existingByPhone.CustomerId != updateCustomerCommand.CustomerId)
                throw new Conflict409Exception($"Заказчик с телефоном '{updateCustomerCommand.Phone}' уже существует.");
        }
        
        return await updateCustomerStorage.UpdateCustomer(updateCustomerCommand.CustomerId, updateCustomerCommand.Name, updateCustomerCommand.Email, updateCustomerCommand.Phone, updateCustomerCommand.CreatedAt, cancellationToken);
    }
}