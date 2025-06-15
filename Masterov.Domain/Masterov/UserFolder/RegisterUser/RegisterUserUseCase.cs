using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.RegisterUser;

public class RegisterUserUseCase(IValidator<RegisterUserCommand> _validator, IRegisterUserStorage registerUserStorage, IGetUserByLoginStorage getUserByLoginStorage) : IRegisterUserUseCase
{
    public async Task<UserDomain> Execute(RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(registerUserCommand, cancellationToken);

        var userExists = await getUserByLoginStorage.GetUserByLogin(registerUserCommand.Login, cancellationToken);
        
        if(userExists is not null)
            throw new UserExistsException();

        return await registerUserStorage.RegisterUser(registerUserCommand.Login, registerUserCommand.Password, cancellationToken);
    }
}