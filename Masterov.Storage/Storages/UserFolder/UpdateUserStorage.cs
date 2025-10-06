using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.UpdateUser;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class UpdateUserStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateUserStorage
{
   public async Task<UserDomain?> UpdateUser(Guid userId, string login, UserRole role, string? newPassword, DateTime? createdAt, Guid? customerId,
        CancellationToken cancellationToken)
    {
        var userExists = await dbContext.Set<Storage.User>().FindAsync([userId], cancellationToken);
        
        if (userExists == null)
            throw new Exception("user not found");
        
        userExists.Login = login;
        if(userExists.Role != UserRole.SuperAdmin)
            userExists.Role = role;
        userExists.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        if (createdAt.HasValue)
            userExists.CreatedAt = createdAt.Value;
        userExists.UpdatedAt = DateTime.Now;
        if(customerId.HasValue)
            userExists.CustomerId = customerId;
        
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