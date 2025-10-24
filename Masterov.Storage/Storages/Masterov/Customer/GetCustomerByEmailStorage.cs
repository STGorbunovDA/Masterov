using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByEmailStorage(MasterovDbContext dbContext, IMapper mapper) 
    : IGetCustomerByEmailStorage
{
    public async Task<CustomerDomain?> GetCustomerByEmail(string customerEmail, CancellationToken cancellationToken)
    {
        var normalizedEmail = customerEmail.Trim().ToLower();
            
        var customer = await dbContext.Customers
            .AsNoTracking() 
            .Where(f => f.Email != null && f.Email.Trim().ToLower() == normalizedEmail)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.UsedComponents)
                    .ThenInclude(pc => pc.ComponentType)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.UsedComponents)
                    .ThenInclude(pc => pc.Warehouse)
            .FirstOrDefaultAsync(cancellationToken);
            
        return mapper.Map<CustomerDomain?>(customer);
    }
}