using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class DeleteUserByLoginStorage(MasterovDbContext dbContext) : IDeleteUserByLoginStorage
{
    public async Task<bool> DeleteUser(string login, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == login, cancellationToken);
        
        if (user == null || user.Role == UserRole.SuperAdmin)
            return false;
        
        dbContext.Set<User>().Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}