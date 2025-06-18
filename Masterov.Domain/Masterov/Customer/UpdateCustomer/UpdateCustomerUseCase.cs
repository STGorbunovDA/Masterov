using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.UpdateCustomer;

public class UpdateCustomerUseCase(IValidator<UpdateCustomerCommand> validator,
    IUpdateCustomerStorage updateCustomerStorage, IGetCustomerByIdStorage getCustomerByIdStorage) : IUpdateCustomerUseCase
{
    public async Task<CustomerDomain> Execute(UpdateCustomerCommand updateCustomerCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateCustomerCommand, cancellationToken);
        
        var customerExists = await getCustomerByIdStorage.GetCustomerById(updateCustomerCommand.CustomerId, cancellationToken);

        if (customerExists is null)
            throw new NotFoundByIdException(updateCustomerCommand.CustomerId, "Заказчик");
        
        return await updateCustomerStorage.UpdateCustomer(updateCustomerCommand.CustomerId, updateCustomerCommand.Name, updateCustomerCommand.Email, updateCustomerCommand.Phone, cancellationToken);
    }
}