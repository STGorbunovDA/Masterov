using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUserByLogin;

public class GetUserByLoginUseCase(IValidator<GetUserByLoginQuery> validator, IGetUserByLoginStorage getUserByLoginStorage) : IGetUserByLoginUseCase
{
    public async Task<UserDomain?> Execute(GetUserByLoginQuery userByLoginQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(userByLoginQuery, cancellationToken);
        var userExists = await getUserByLoginStorage.GetUserByLogin(userByLoginQuery.Login, cancellationToken);
        
        if (userExists is null)
            throw new NotFoundByLoginException(userByLoginQuery.Login);
        
        return userExists;
    }
}