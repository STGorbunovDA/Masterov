using AutoMapper;
using Masterov.Domain.Masterov.Order.GetOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetOrdersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetOrdersStorage
{
    public async Task<IEnumerable<OrderDomain>> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Include(o => o.FinishedProduct)
            .Select(o => new OrderDomain
            {
                OrderId = o.OrderId,
                CreatedAt = o.CreatedAt,
                CompletedAt = o.CompletedAt,
                Status = o.Status,
                Description = o.Description,
                FullPriceFinishedProduct = o.FinishedProduct.Price,
                Customer = mapper.Map<CustomerDomain>(o.Customer),
                Components = o.Components.Select(c => mapper.Map<ProductComponentDomain>(c)),
                Payments = o.Payments.Select(p => mapper.Map<PaymentDomain>(p))
            })
            .ToListAsync(cancellationToken);

        return orders;
    }
}