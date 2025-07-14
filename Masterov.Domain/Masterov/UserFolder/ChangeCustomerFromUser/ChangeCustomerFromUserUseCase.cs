using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;

public class ChangeCustomerFromUserUseCase(IValidator<ChangeCustomerFromUserCommand> validator, 
    IChangeCustomerFromUserStorage storage, 
    IGetUserByIdStorage getUserByIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage) : IChangeCustomerFromUserUseCase
{
    public async Task<UserDomain?> Execute(ChangeCustomerFromUserCommand customerFromUserCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(customerFromUserCommand, cancellationToken);
        
        var userExists = await getUserByIdStorage.GetUserById(customerFromUserCommand.UserId, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByIdException(customerFromUserCommand.UserId, "Пользователь");
        
        var customerExists = await getCustomerByIdStorage.GetCustomerById(customerFromUserCommand.CustomerId, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByIdException(customerFromUserCommand.CustomerId, "Заказчик");

        return await storage.ChangeCustomerFromUser(customerFromUserCommand.UserId, customerFromUserCommand.CustomerId,
            cancellationToken);
    }
}