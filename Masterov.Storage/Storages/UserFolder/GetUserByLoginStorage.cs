using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUserByLoginStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUserByLoginStorage
{
    public async Task<UserDomain?> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
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
    }
}