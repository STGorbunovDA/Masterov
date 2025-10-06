using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetCustomerByIdStorage
{
    public async Task<CustomerDomain?> GetCustomerById(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .AsNoTracking()
            .Where(f => f.CustomerId == customerId)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                    .ThenInclude(p => p.Customer)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Components)
                    .ThenInclude(pc => pc.ProductType)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Components)
                    .ThenInclude(pc => pc.Warehouse)
            .FirstOrDefaultAsync(cancellationToken);
            
        return mapper.Map<CustomerDomain>(customer);
    }
}