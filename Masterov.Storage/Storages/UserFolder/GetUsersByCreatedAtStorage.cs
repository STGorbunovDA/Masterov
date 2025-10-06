using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUsersByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsersByCreatedAtStorage
{
    public async Task<IEnumerable<UserDomain>?> GetUsersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var users = await dbContext.Users
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
                .Include(c => c.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UserDomain>>(users);
    }
}