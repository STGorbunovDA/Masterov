using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetComponentsByOrderIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentsByOrderIdStorage
{
    public async Task<IEnumerable<ComponentsDomain?>> GetComponentsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.Components
            .AsNoTracking() 
            .Where(pc => pc.OrderId == orderId)
            .ProjectTo<ComponentsDomain>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}