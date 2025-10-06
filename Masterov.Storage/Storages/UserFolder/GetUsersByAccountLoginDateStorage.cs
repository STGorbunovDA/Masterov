using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class GetUsersByAccountLoginDateStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsersByAccountLoginDateStorage
{
    public async Task<IEnumerable<UserDomain>?> GetUsersByAccountLoginDate(DateTime? accountLoginDate, CancellationToken cancellationToken)
    {
        if (!accountLoginDate.HasValue)
            return null;
        
        var startOfDay = accountLoginDate.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var users = await dbContext.Users
            .AsNoTracking() 
            .Where(order => order.AccountLoginDate >= startOfDay && order.AccountLoginDate < endOfDay)
                .Include(c => c.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UserDomain>>(users);
    }
}