using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.AddCustomer.Command;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.AddCustomer;

public class AddCustomerUseCase(
    IValidator<AddCustomerCommand> validator,
    IAddCustomerStorage addCustomerStorage, 
    IGetCustomerByPhoneStorage getCustomerByPhoneStorage,
    IGetCustomerByEmailStorage getCustomerByEmailStorage) : IAddCustomerUseCase
{
    public async Task<CustomerDomain> Execute(AddCustomerCommand addCustomerCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addCustomerCommand, cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(addCustomerCommand.Email))
        {
            var customerByEmail = await getCustomerByEmailStorage.GetCustomerByEmail(
                addCustomerCommand.Email, cancellationToken);
            if (customerByEmail is not null)
                throw new CustomerExistsException(customerByEmail.Name, customerByEmail.Email, customerByEmail.Phone);
        }
    
        if (!string.IsNullOrWhiteSpace(addCustomerCommand.Phone))
        {
            var customerByPhone = await getCustomerByPhoneStorage.GetCustomerByPhone(
                addCustomerCommand.Phone, cancellationToken);
            if (customerByPhone is not null)
                throw new CustomerExistsException(customerByPhone.Name, customerByPhone.Email, customerByPhone.Phone);
        }

        return await addCustomerStorage.AddCustomer(addCustomerCommand.Name,
            addCustomerCommand?.Phone, addCustomerCommand?.Email, addCustomerCommand?.UserId, cancellationToken);
    }
}