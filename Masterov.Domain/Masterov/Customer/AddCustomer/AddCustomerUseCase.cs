using FluentValidation;
using Masterov.Domain.Masterov.Customer.AddCustomer.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.AddCustomer;

public class AddCustomerUseCase(
    IValidator<AddCustomerCommand> validator,
    IAddCustomerStorage addCustomerStorage) : IAddCustomerUseCase
{
    public async Task<CustomerDomain> Execute(AddCustomerCommand addCustomerCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addCustomerCommand, cancellationToken);
        
        return await addCustomerStorage.AddCustomer(addCustomerCommand.Name,
            addCustomerCommand?.Email, addCustomerCommand?.Phone, cancellationToken);
    }
}