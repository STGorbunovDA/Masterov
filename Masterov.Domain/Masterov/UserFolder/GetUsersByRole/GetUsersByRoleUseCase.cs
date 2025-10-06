using FluentValidation;
using Masterov.Domain.Masterov.UserFolder.GetUsersByRole.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByRole;

public class GetUsersByRoleUseCase(IValidator<GetUsersByRoleQuery> validator, IGetUsersByRoleStorage storage) : IGetUsersByRoleUseCase
{
    public async Task<IEnumerable<UserDomain>?> Execute(GetUsersByRoleQuery usersByRoleQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(usersByRoleQuery, cancellationToken);
        
        return await storage.GetUsersByRole(usersByRoleQuery.role, cancellationToken);
    }
}