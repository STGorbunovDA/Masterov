using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetOrderByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetOrderByIdStorage
{
    public async Task<OrderDomain?> GetOrderById(Guid orderId, CancellationToken cancellationToken) =>
        await dbContext.Orders
            .AsNoTracking()
            .Where(f => f.OrderId == orderId)
            .ProjectTo<OrderDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}