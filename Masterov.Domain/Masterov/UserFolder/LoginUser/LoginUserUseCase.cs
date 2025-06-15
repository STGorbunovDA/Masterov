using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.LoginUser.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.LoginUser;

public class LoginUserUseCase(IValidator<GetLoginUserQuery> validator, IGetUserByLoginStorage getUserByLoginStorage) : ILoginUserUseCase
{
    public async Task<UserDomain> Execute(GetLoginUserQuery getLoginUserQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getLoginUserQuery, cancellationToken);

        var userExists = await getUserByLoginStorage.GetUserByLogin(getLoginUserQuery.Login, cancellationToken);
        
        if(userExists is null)
            throw new NotFoundByLoginException(getLoginUserQuery.Login);

        if (!BCrypt.Net.BCrypt.Verify(getLoginUserQuery.Password, userExists.PasswordHash))
            throw new UserInvalidPasswordException();

        return userExists;
    }
}