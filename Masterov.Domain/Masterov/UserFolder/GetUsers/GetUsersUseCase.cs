using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsers;

public class GetUsersUseCase(IGetUsersStorage storage) : IGetUsersUseCase
{
    public async Task<IEnumerable<UserDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetUsers(cancellationToken);
}