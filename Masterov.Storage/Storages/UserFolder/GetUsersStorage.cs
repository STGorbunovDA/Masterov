using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUsersStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetUsersStorage
{
    public async Task<IEnumerable<UserDomain>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await dbContext.Users
            .AsNoTracking()
            .Where(user => user.Role != UserRole.SuperAdmin)
                .Include(u => u.Customer)
                    .ThenInclude(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                    .ThenInclude(p => p.Customer)
            .ToListAsync(cancellationToken);

        var mappedUsers = mapper.Map<UserDomain[]>(users);

        return mappedUsers ?? Array.Empty<UserDomain>();
    }
}