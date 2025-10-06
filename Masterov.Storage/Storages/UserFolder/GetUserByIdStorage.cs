using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUserByIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUserByIdStorage
{
    public async Task<UserDomain?> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var userEntity = await dbContext.Users
            .AsNoTracking()
            .Where(f => f.UserId == userId)
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