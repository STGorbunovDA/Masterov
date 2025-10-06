using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;

public class ChangeRoleUserByLoginUseCase(IValidator<ChangeRoleUserByLoginCommand> validator, 
    IChangeRoleUserByIdStorage changeRoleUserByIdStorage, 
    IGetUserByLoginStorage getUserByLoginStorage) : IChangeRoleUserByLoginUseCase
{
    public async Task<UserDomain> Execute(ChangeRoleUserByLoginCommand changeRoleUserByLoginCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(changeRoleUserByLoginCommand, cancellationToken);

        var userExists = await getUserByLoginStorage.GetUserByLogin(changeRoleUserByLoginCommand.Login, cancellationToken);
        
        if(userExists is null)
            throw new NotFoundByLoginException(changeRoleUserByLoginCommand.Login);

        return await changeRoleUserByIdStorage.ChangeRoleUserById(userExists.UserId, changeRoleUserByLoginCommand.Role, cancellationToken);
    }
}