using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUser;

public class ChangeRoleUserUseCase(IValidator<ChangeRoleUserCommand> validator, IChangeRoleUserStorage changeRoleUserStorage, IGetUserByLoginStorage getUserByLoginStorage) : IChangeRoleUserUseCase
{
    public async Task<UserDomain> Execute(ChangeRoleUserCommand changeRoleUserCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(changeRoleUserCommand, cancellationToken);

        var userExists = await getUserByLoginStorage.GetUserByLogin(changeRoleUserCommand.Login, cancellationToken);
        
        if(userExists is null)
            throw new NotFoundByLoginException(changeRoleUserCommand.Login);

        return await changeRoleUserStorage.ChangeRoleUser(userExists.UserId, changeRoleUserCommand.Role, cancellationToken);
    }
}