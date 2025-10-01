using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class AddCustomerStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddCustomerStorage
{
    public async Task<CustomerDomain> AddCustomer(string name, string? phone, string? email, 
        CancellationToken cancellationToken)
    {
        var customerId = guidFactory.Create();

        var customer = new Storage.Customer
        {
            CustomerId = customerId,
            CreatedAt = DateTime.Now,
            Name = name,
            Phone = phone,
            Email = email
        };

        await dbContext.Customers.AddAsync(customer, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Customers
            .Where(t => t.CustomerId == customerId)
            .ProjectTo<CustomerDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}