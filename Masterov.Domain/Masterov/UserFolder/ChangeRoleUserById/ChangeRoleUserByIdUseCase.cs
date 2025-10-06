using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;

public class ChangeRoleUserByIdUseCase(IValidator<ChangeRoleUserByIdCommand> validator, 
    IChangeRoleUserByIdStorage changeRoleUserByIdStorage,
    IGetUserByIdStorage getUserByIdStorage) : IChangeRoleUserByIdUseCase
{
    public async Task<UserDomain> Execute(ChangeRoleUserByIdCommand changeRoleUserByIdCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(changeRoleUserByIdCommand, cancellationToken);

        var userExists = await getUserByIdStorage.GetUserById(changeRoleUserByIdCommand.UserId, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByIdException(changeRoleUserByIdCommand.UserId, "Пользователь");

        return await changeRoleUserByIdStorage.ChangeRoleUserById(userExists.UserId, changeRoleUserByIdCommand.Role, cancellationToken);
    }
}