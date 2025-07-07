using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Users
                    .AsNoTracking() 
                    .Where(user => user.Role != UserRole.SuperAdmin) 
                    .ProjectTo<UserDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}