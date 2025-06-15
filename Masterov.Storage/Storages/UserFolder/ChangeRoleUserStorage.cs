using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

public class ChangeRoleUserStorage (MasterovDbContext dbContext, IMapper mapper) : IChangeRoleUserStorage
{
    public async Task<UserDomain> ChangeRoleUser(Guid userId, UserRole role, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(f => f.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
            throw new Exception("user not found");

        user.Role = role;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<UserDomain>(user);
    }
}