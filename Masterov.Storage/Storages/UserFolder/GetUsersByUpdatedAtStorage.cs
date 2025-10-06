using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUsersByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsersByUpdatedAtStorage
{
    public async Task<IEnumerable<UserDomain>?> GetUsersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var users = await dbContext.Users
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
                .Include(c => c.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UserDomain>>(users);
    }
}