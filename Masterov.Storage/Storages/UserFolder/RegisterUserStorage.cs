using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.UserFolder;

internal class RegisterUserStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IRegisterUserStorage
{
    public async Task<UserDomain> RegisterUser(string login, string password, Guid? customerId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<UserDomain>( 
            nameof(RegisterUser),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = login,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = UserRole.RegularUser,
                    CustomerId = customerId
                };
                
                await dbContext.Users.AddAsync(user, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                var userDb = await dbContext.Users
                    .AsNoTracking()
                    .Where(t => t.UserId == user.UserId)
                        .Include(c => c.Customer)
                    .FirstOrDefaultAsync(cancellationToken);

                if (userDb is null)
                    throw new InvalidOperationException("Не удалось найти только что зарегистрированного пользователя.");

                return mapper.Map<UserDomain>(userDb);
            }))!;
}
