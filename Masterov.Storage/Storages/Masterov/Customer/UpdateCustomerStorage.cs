using AutoMapper;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class UpdateCustomerStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateCustomerStorage
{
    public async Task<CustomerDomain> UpdateCustomer(Guid customerId, string name, string? email, string? phone, CancellationToken cancellationToken)
    {
        var customerExists = await dbContext.Set<Storage.Customer>().FindAsync([customerId], cancellationToken);
        
        if (customerExists == null)
            throw new Exception("customer not found");
        
        customerExists.Name = name;
        customerExists.Email = email;
        customerExists.Phone = phone;
        customerExists.UpdatedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<CustomerDomain>(customerExists);
    }
}