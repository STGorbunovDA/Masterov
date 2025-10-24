using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.RegisterUser;

public class RegisterUserUseCase(
    IValidator<RegisterUserCommand> validator,
    IRegisterUserStorage registerUserStorage,
    IGetUserByLoginStorage getUserByLoginStorage) : IRegisterUserUseCase
{
    public async Task<UserDomain> Execute(RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(registerUserCommand, cancellationToken);

        var userExists = await getUserByLoginStorage.GetUserByLogin(registerUserCommand.Email, cancellationToken);

        if (userExists is not null)
            throw new UserExistsException();

        return await registerUserStorage.RegisterUser(registerUserCommand.Email, registerUserCommand.Password,
            registerUserCommand?.CustomerId, cancellationToken);
    }
}