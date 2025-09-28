using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetCustomerByNameStorage
{
    public async Task<CustomerDomain?> GetCustomerByName(string customerName, CancellationToken cancellationToken)
    {
        var normalizedName = customerName.Trim().ToLower();
        
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
            .Where(f => f.Name.ToLower() == normalizedName)
            .FirstOrDefaultAsync(cancellationToken);
            
        return mapper.Map<CustomerDomain>(customer);
    }
}