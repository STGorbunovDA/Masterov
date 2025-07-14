using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;

namespace Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;

public class ChangePasswordFromUserUseCase(IValidator<ChangePasswordFromUserCommand> validator, 
    IChangePasswordFromUserStorage storage, 
    IGetUserByIdStorage getUserByIdStorage) : IChangePasswordFromUserUseCase
{
    public async Task<bool> Execute(ChangePasswordFromUserCommand changePasswordFromUserCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(changePasswordFromUserCommand, cancellationToken);
        
        var userExists = await getUserByIdStorage.GetUserById(changePasswordFromUserCommand.UserId, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByIdException(changePasswordFromUserCommand.UserId, "Пользователь");
        
        if (!BCrypt.Net.BCrypt.Verify(changePasswordFromUserCommand.OldPassword, userExists.PasswordHash))
            throw new UserInvalidPasswordException();

        return await storage.ChangePasswordFromUser(changePasswordFromUserCommand.UserId, changePasswordFromUserCommand.NewPassword,
            cancellationToken);
    }
}