using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUserByIdStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetUserByIdStorage
{
    public async Task<UserDomain?> GetUserById(Guid userId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<UserDomain?>( 
            nameof(GetUserById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Users
                    .Where(f => f.UserId == userId)
                    .ProjectTo<UserDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}