using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByPhoneStorage(MasterovDbContext dbContext, IMapper mapper) : IGetCustomerByPhoneStorage
{
    public async Task<CustomerDomain?> GetCustomerByPhone(string customerPhone, CancellationToken cancellationToken)
    {
        var normalizedPhone = customerPhone.Trim().ToLower();
        
        var customer = await dbContext.Customers
            .AsNoTracking() 
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                    .ThenInclude(p => p.Customer)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Components)
                    .ThenInclude(pc => pc.ProductType)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Components)
                    .ThenInclude(pc => pc.Warehouse)
            .Where(f => f.Phone != null && f.Phone.Trim().ToLower() == normalizedPhone)
            .FirstOrDefaultAsync(cancellationToken);
            
        return mapper.Map<CustomerDomain>(customer);
    }
}