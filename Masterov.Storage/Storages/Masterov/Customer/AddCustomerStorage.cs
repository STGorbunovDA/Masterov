using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class AddCustomerStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddCustomerStorage
{
    public async Task<CustomerDomain> AddCustomer(string name, string? email, string? phone, 
        CancellationToken cancellationToken, Guid? orderId = null)
    {
        var customerId = guidFactory.Create();

        // Создаем нового клиента
        var customer = new Storage.Customer
        {
            CustomerId = customerId,
            Name = name,
            Email = email,
            Phone = phone,
            Orders = new List<Storage.ProductionOrder>()
        };

        // Если указан orderId, связываем существующий заказ с клиентом
        if (orderId.HasValue)
        {
            var existingOrder = await dbContext.ProductionOrders
                .FirstOrDefaultAsync(o => o.OrderId == orderId.Value, cancellationToken);

            if (existingOrder != null)
            {
                existingOrder.Customer = customer;
                customer.Orders.Add(existingOrder);
            }
        }

        await dbContext.Customers.AddAsync(customer, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Customers
            .Where(t => t.CustomerId == customerId)
            .ProjectTo<CustomerDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}