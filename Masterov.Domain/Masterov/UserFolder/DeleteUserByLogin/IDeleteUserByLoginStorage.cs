namespace Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;

public interface IDeleteUserByLoginStorage
{
    Task<bool> DeleteUser(string login, CancellationToken cancellationToken);
}