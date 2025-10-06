using FluentValidation;
using Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt;

public class GetUsersByUpdatedAtUseCase(IValidator<GetUsersByUpdatedAtQuery> validator,
    IGetUsersByUpdatedAtStorage storage) : IGetUsersByUpdatedAtUseCase
{
    public async Task<IEnumerable<UserDomain>?> Execute(GetUsersByUpdatedAtQuery byUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(byUpdatedAtQuery, cancellationToken);
        
        return await storage.GetUsersByUpdatedAt(byUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}