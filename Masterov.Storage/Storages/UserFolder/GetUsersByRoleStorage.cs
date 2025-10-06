using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.GetUsersByRole;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

public class GetUsersByRoleStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsersByRoleStorage
{
    public async Task<IEnumerable<UserDomain>?> GetUsersByRole(UserRole role, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Users
            .Where(r => r.Role == role && r.Role != UserRole.SuperAdmin)
                .Include(o => o.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UserDomain>>(orders);
    }
}