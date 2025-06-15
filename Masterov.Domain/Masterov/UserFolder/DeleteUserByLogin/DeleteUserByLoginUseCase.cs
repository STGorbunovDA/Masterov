using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;

namespace Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;

public class DeleteUserByLoginUseCase(IValidator<DeleteUserByLoginCommand> validator, 
    IDeleteUserByLoginStorage deleteUserByLoginStorage, IGetUserByLoginStorage getUserByloginStorage) : IDeleteUserByLoginUseCase
{
    public async Task<bool> Execute(DeleteUserByLoginCommand deleteUserByLoginCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteUserByLoginCommand, cancellationToken);
        
        var userExists = await getUserByloginStorage.GetUserByLogin(deleteUserByLoginCommand.Login, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByLoginException(deleteUserByLoginCommand.Login);
        
        return await deleteUserByLoginStorage.DeleteUser(deleteUserByLoginCommand.Login, cancellationToken);
    }
}