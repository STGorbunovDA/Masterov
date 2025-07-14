using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

public class ChangeCustomerFromUserStorage (MasterovDbContext dbContext, IMapper mapper) : IChangeCustomerFromUserStorage
{
    public async Task<UserDomain?> ChangeCustomerFromUser(Guid userId, Guid customerId, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(f => f.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
            throw new Exception("user not found");

        user.CustomerId = customerId;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
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