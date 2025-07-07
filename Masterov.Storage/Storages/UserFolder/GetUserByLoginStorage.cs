using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUserByLoginStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetUserByLoginStorage
{
    public async Task<UserDomain?> GetUserByLogin(string login, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<UserDomain?>( 
            nameof(GetUserByLogin),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Users
                    .AsNoTracking() 
                    .Where(f => f.Login == login)
                    .ProjectTo<UserDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}