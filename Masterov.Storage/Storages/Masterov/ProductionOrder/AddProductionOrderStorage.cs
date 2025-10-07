using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class AddProductionOrderStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddProductionOrderStorage
{
    public async Task<OrderDomain> AddProductionOrder(Guid finishedProductId, Guid customerId, string? description,
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

        var order = new Storage.ProductionOrder
        {
            OrderId = guidFactory.Create(),
            FinishedProductId = finishedProductId,
            CustomerId = customerId,
            Description = description,
            Status = ProductionOrderStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };

        await dbContext.ProductionOrders.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var orderDb = await dbContext.ProductionOrders
            .AsNoTracking()
            .Where(t => t.OrderId == order.OrderId)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(cancellationToken);
        
        return mapper.Map<OrderDomain>(orderDb);
    }
}