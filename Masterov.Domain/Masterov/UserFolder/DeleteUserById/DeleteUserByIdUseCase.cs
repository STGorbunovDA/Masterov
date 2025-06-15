using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;

namespace Masterov.Domain.Masterov.UserFolder.DeleteUserById;

public class DeleteUserByIdUseCase(IValidator<DeleteUserByIdCommand> validator, 
    IDeleteUserByIdStorage byIdStorage, IGetUserByIdStorage getUserByIdStorage) : IDeleteUserByIdUseCase
{
    public async Task<bool> Execute(DeleteUserByIdCommand deleteUserByIdCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteUserByIdCommand, cancellationToken);
        
        var userExists = await getUserByIdStorage.GetUserById(deleteUserByIdCommand.UserId, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByIdException(deleteUserByIdCommand.UserId, "Пользователь");
        
        return await byIdStorage.DeleteUser(deleteUserByIdCommand.UserId, cancellationToken);
    }
}