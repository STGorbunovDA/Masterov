using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.ChangeUpdatedAtUserById.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById;

public class ChangeAccountLoginDateUserByIdUseCase(IValidator<ChangeAccountLoginDateUserByIdCommand> validator, 
    IChangeAccountLoginDateUserByIdStorage storage, 
    IGetUserByIdStorage getUserByIdStorage) : IChangeAccountLoginDateUserByIdUseCase
{
    public async Task<UserDomain?> Execute(ChangeAccountLoginDateUserByIdCommand accountLoginDateUserByIdCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(accountLoginDateUserByIdCommand, cancellationToken);
        
        var userExists = await getUserByIdStorage.GetUserById(accountLoginDateUserByIdCommand.UserId, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByIdException(accountLoginDateUserByIdCommand.UserId, "Пользователь");

        return await storage.ChangeAccountLoginDateUserById(
            accountLoginDateUserByIdCommand.UserId, 
            accountLoginDateUserByIdCommand.AccountLoginDate,
            cancellationToken);
    }
}