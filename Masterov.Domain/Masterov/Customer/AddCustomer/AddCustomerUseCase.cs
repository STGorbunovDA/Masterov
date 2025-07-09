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
        
        CustomerDomain? customer = null;

        if (addCustomerCommand.Phone is not null)
            customer = await getCustomerByPhoneStorage.GetCustomerByPhone(addCustomerCommand.Phone, cancellationToken);
        else if (customer is null && addCustomerCommand.Email is not null)
            customer = await getCustomerByEmailStorage.GetCustomerByEmail(addCustomerCommand.Email, cancellationToken);

        if (customer is not null)
            if (customer is { Email: not null, Phone: not null })
                throw new CustomerExistsException(customer.Name, customer.Email, customer.Phone);

        return await addCustomerStorage.AddCustomer(addCustomerCommand.Name,
            addCustomerCommand?.Email, addCustomerCommand?.Phone, cancellationToken);
    }
}