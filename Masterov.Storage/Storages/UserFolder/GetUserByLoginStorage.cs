using AutoMapper;
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
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                var userEntity = await dbContext.Users
                    .AsNoTracking()
                    .Where(f => f.Login == login)
                        .Include(u => u.Customer)
                            .ThenInclude(c => c.Orders)
                                .ThenInclude(o => o.Payments)
                                    .ThenInclude(p => p.Customer)
                    .FirstOrDefaultAsync(cancellationToken);

                return userEntity == null
                    ? null
                    : mapper.Map<UserDomain>(userEntity);
            }))!;
}