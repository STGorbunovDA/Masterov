using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById;

namespace Masterov.Storage.Storages.UserFolder;

internal class DeleteUserByIdStorage(MasterovDbContext dbContext) : IDeleteUserByIdStorage
{
    public async Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>().FindAsync(new object[] { userId }, cancellationToken);
        
        if (user == null || user.Role == UserRole.SuperAdmin)
            return false;
        
        dbContext.Set<User>().Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}