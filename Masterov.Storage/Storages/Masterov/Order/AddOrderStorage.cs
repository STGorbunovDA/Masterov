using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.AddOrder;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class AddOrderStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddOrderStorage
{
    public async Task<OrderDomain> AddOrder(Guid finishedProductId, Guid customerId, string? description,
        CancellationToken cancellationToken)
    {
        var finishedProductExists = await dbContext.FinishedProducts
            .AnyAsync(fp => fp.FinishedProductId == finishedProductId, cancellationToken);
        
        if (!finishedProductExists)
        {
            throw new ArgumentException("Finished product not found", nameof(finishedProductId));
        }

        var customerExists = await dbContext.Customers
            .AnyAsync(c => c.CustomerId == customerId, cancellationToken);
        
        if (!customerExists)
        {
            throw new ArgumentException("Customer not found", nameof(customerId));
        }

        var order = new Storage.Order
        {
            OrderId = guidFactory.Create(),
            FinishedProductId = finishedProductId,
            CustomerId = customerId,
            Description = description,
            Status = OrderStatus.Draft,
            CreatedAt = DateTime.Now
        };

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var orderDb = await dbContext.Orders
            .AsNoTracking()
            .Where(t => t.OrderId == order.OrderId)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(cancellationToken);
        
        return mapper.Map<OrderDomain>(orderDb);
    }
}