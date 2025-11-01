using AutoMapper;
using Masterov.Domain.Masterov.Order.GetOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetOrdersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetOrdersStorage
{
    public async Task<IEnumerable<OrderDomain?>> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Include(o => o.FinishedProduct)
            .Include(o => o.Customer)
            .Include(o => o.UsedComponents)
                .ThenInclude(c => c.ComponentType)
            .Include(o => o.UsedComponents)
                .ThenInclude(c => c.Warehouse)
            .Include(o => o.Payments)
            .ToListAsync(cancellationToken);

        return orders.Select(o => mapper.Map<OrderDomain>(o));
    }
}