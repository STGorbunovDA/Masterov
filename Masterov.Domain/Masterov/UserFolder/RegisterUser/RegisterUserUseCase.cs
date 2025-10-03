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
    IGetUserByLoginStorage getUserByLoginStorage,
    IGetCustomerByEmailStorage getCustomerByEmailStorage,
    IGetCustomerByPhoneStorage getCustomerByPhoneStorage,
    IUpdateCustomerStorage updateCustomerStorage) : IRegisterUserUseCase
{
    public async Task<UserDomain> Execute(RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(registerUserCommand, cancellationToken);

        var userExists = await getUserByLoginStorage.GetUserByLogin(registerUserCommand.Email, cancellationToken);

        if (userExists is not null)
            throw new UserExistsException();

        var customerDomain =
            await getCustomerByEmailStorage.GetCustomerByEmail(registerUserCommand.Email, cancellationToken);

        if (customerDomain is null)
        {
            customerDomain =
                await getCustomerByPhoneStorage.GetCustomerByPhone(registerUserCommand.Phone, cancellationToken);

            if (customerDomain is not null)
            {
                await updateCustomerStorage.UpdateCustomer(customerDomain.CustomerId, customerDomain.Name,
                    registerUserCommand.Email, customerDomain.Phone, customerDomain.CreatedAt, cancellationToken);
            }
        }

        return await registerUserStorage.RegisterUser(registerUserCommand.Email, registerUserCommand.Password,
            customerDomain?.CustomerId, cancellationToken);
    }
}