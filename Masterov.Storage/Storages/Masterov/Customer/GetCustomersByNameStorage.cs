using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomersByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomersByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetCustomerByNameStorage
{
    public async Task<IEnumerable<CustomerDomain?>> GetCustomersByName(string customerName, CancellationToken cancellationToken)
    {
        var normalizedName = customerName.Trim().ToLower();
        
        var customers = await dbContext.Customers
            .AsNoTracking() 
            .Where(f => f.Name.ToLower() == normalizedName)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                    .ThenInclude(p => p.Customer)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.UsedComponents)
                    .ThenInclude(pc => pc.ComponentType)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.UsedComponents)
                    .ThenInclude(pc => pc.Warehouse)
            .ToArrayAsync(cancellationToken);
            
        return mapper.Map<IEnumerable<CustomerDomain>>(customers);
    }
}