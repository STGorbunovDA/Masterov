using Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.RegisterUser;

public interface IRegisterUserUseCase
{
    Task<UserDomain> Execute(RegisterUserCommand registerUserCommand, CancellationToken cancellationToken);
}