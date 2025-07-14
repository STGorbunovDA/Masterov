using AutoMapper;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

public class ChangePasswordFromUserStorage (MasterovDbContext dbContext, IMapper mapper) : IChangePasswordFromUserStorage
{
    public async Task<bool> ChangePasswordFromUser(Guid userId, string newPassword, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(f => f.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
            throw new Exception("user not found");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}