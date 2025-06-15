using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.UserFolder;

internal class RegisterUserStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IRegisterUserStorage
{
    public async Task<UserDomain> RegisterUser(string login, string password, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<UserDomain>( 
            nameof(RegisterUser),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Login = login,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = UserRole.RegularUser
                };
                
                await dbContext.Users.AddAsync(user, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                return mapper.Map<UserDomain>(user);
            }))!;
}
