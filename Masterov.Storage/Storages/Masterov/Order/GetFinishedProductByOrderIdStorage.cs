using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetFinishedProductByOrderIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductByOrderIdStorage
{
    public async Task<FinishedProductDomain?> GetFinishedProductByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking() 
            .Where(o => o.OrderId == orderId)
            .Select(o => o.FinishedProduct)
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}