using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetUsedComponentsByOrderIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsByOrderIdStorage
{
    public async Task<IEnumerable<UsedComponentDomain?>> GetUsedComponentsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(pc => pc.OrderId == orderId)
            .ProjectTo<UsedComponentDomain>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}