using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

public class ChangeRoleUserByIdStorage (MasterovDbContext dbContext, IMapper mapper) : IChangeRoleUserByIdStorage
{
    public async Task<UserDomain> ChangeRoleUserById(Guid userId, UserRole role, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(f => f.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
            throw new Exception("user not found");

        if(role != UserRole.SuperAdmin)
            user.Role = role;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<UserDomain>(user);
    }
}