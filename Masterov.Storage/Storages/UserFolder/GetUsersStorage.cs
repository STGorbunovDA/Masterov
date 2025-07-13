using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUsersStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper)
    : IGetUsersStorage
{
    public async Task<IEnumerable<UserDomain>> GetUsers(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<UserDomain[]>(
            nameof(GetUsers),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var users = await dbContext.Users
                    .AsNoTracking()
                    .Where(user => user.Role != UserRole.SuperAdmin)
                        .Include(u => u.Customer)
                            .ThenInclude(c => c.Orders)
                                .ThenInclude(o => o.Payments)
                                    .ThenInclude(p => p.Customer)
                    .ToListAsync(cancellationToken);

                var mappedUsers = mapper.Map<UserDomain[]>(users);

                // Обработка потенциального null после маппинга
                return mappedUsers ?? Array.Empty<UserDomain>();
            }))!;
}