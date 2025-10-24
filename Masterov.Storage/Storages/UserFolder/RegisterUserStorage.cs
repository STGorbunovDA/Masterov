using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.UserFolder;

internal class RegisterUserStorage (MasterovDbContext dbContext, IMapper mapper) : IRegisterUserStorage
{
    public async Task<UserDomain> RegisterUser(string login, string password, Guid? customerId, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Login = login,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = UserRole.RegularUser,
            CreatedAt = DateTime.Now
        };
        
        if (customerId.HasValue)
        {
            var customer = await dbContext.Customers
                .FirstOrDefaultAsync(u => u.CustomerId == customerId.Value, cancellationToken);

            if (customer is not null)
            {
                customer.UserId = user.UserId;
                user.CustomerId = customerId;
            }
        }
        
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
    }
}