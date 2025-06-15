using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.GetUserById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUserById;

public class GetUserByIdUseCase(IValidator<GetUserByIdQuery> validator, IGetUserByIdStorage byIdStorage) : IGetUserByIdUseCase
{
    public async Task<UserDomain?> Execute(GetUserByIdQuery getUserByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getUserByIdQuery, cancellationToken);
        var userExists = await byIdStorage.GetUserById(getUserByIdQuery.UserId, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByIdException(getUserByIdQuery.UserId, "Пользователь");
        
        return userExists;
    }
}