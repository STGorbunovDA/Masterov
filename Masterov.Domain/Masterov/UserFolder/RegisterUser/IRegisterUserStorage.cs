using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.RegisterUser;

public interface IRegisterUserStorage
{
    Task<UserDomain> RegisterUser(string login, string password, CancellationToken cancellationToken);
}