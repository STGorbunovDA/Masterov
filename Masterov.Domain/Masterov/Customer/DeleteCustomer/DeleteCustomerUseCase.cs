using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.DeleteCustomer.Command;
using Masterov.Domain.Masterov.Customer.GetCustomerById;

namespace Masterov.Domain.Masterov.Customer.DeleteCustomer;

public class DeleteCustomerUseCase(IValidator<DeleteCustomerCommand> validator, 
    IDeleteCustomerStorage storage, IGetCustomerByIdStorage getCustomerByIdStorage) : IDeleteCustomerUseCase
{
    public async Task<bool> Execute(DeleteCustomerCommand deleteCustomerCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteCustomerCommand, cancellationToken);
        
        var customerExists = await getCustomerByIdStorage.GetCustomerById(deleteCustomerCommand.CustomerId, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByIdException(deleteCustomerCommand.CustomerId, "Заказчик");
        
        return await storage.DeleteCustomer(deleteCustomerCommand.CustomerId, cancellationToken);
    }
}