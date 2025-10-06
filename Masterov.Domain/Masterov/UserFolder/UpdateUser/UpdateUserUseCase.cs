using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Masterov.UserFolder.UpdateUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.UpdateUser;

public class UpdateUserUseCase(
    IValidator<UpdateUserCommand> validator,
    IUpdateUserStorage updateUserStorage,
    IGetUserByIdStorage getUserByIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage) : IUpdateUserUseCase
{
    public async Task<UserDomain?> Execute(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateUserCommand, cancellationToken);

        var userExists = await getUserByIdStorage.GetUserById(updateUserCommand.UserId, cancellationToken);

        if (userExists is null)
            throw new NotFoundByIdException(updateUserCommand.UserId, "Пользователь");

        if (updateUserCommand.CustomerId is not null)
        {
            var customerExists =
                await getCustomerByIdStorage.GetCustomerById(updateUserCommand.CustomerId.Value, cancellationToken);

            if (customerExists is null)
                throw new NotFoundByIdException(updateUserCommand.CustomerId.Value, "Заказчик");
        }

        if (!BCrypt.Net.BCrypt.Verify(updateUserCommand.OldPassword, userExists.PasswordHash))
            throw new UserInvalidPasswordException();

        return await updateUserStorage.UpdateUser(
            updateUserCommand.UserId, updateUserCommand.Login, updateUserCommand.Role, updateUserCommand.NewPassword,
            updateUserCommand.CreatedAt, updateUserCommand.CustomerId, cancellationToken);
    }
}