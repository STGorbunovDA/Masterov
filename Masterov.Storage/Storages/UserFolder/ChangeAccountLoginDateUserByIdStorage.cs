using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class ChangeAccountLoginDateUserByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IChangeAccountLoginDateUserByIdStorage
{
   public async Task<UserDomain?> ChangeAccountLoginDateUserById(Guid userId, DateTime? accountLoginDate, CancellationToken cancellationToken)
    {
        var userExists = await dbContext.Set<Storage.User>().FindAsync([userId], cancellationToken);
        
        if (userExists == null)
            throw new Exception("user not found");
        
        if (accountLoginDate.HasValue)
            userExists.AccountLoginDate = accountLoginDate.Value;
        
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