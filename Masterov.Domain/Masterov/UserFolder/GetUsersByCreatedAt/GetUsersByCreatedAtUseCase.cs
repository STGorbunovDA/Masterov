using FluentValidation;
using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt;

public class GetUsersByCreatedAtUseCase(IValidator<GetUsersByCreatedAtQuery> validator,
    IGetUsersByCreatedAtStorage storage) : IGetUsersByCreatedAtUseCase
{
    public async Task<IEnumerable<UserDomain>?> Execute(GetUsersByCreatedAtQuery usersByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(usersByCreatedAtQuery, cancellationToken);
        
        return await storage.GetUsersByCreatedAt(usersByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}