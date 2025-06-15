namespace Masterov.Domain.Masterov.UserFolder.DeleteUserById;

public interface IDeleteUserByIdStorage
{
    Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken);
}