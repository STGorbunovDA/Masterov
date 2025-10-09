using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetProductComponentByOrderIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetProductComponentByOrderIdStorage
{
    public async Task<IEnumerable<ProductComponentDomain?>> GetProductComponentByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.ProductComponents
            .AsNoTracking() 
            .Where(pc => pc.OrderId == orderId)
            .ProjectTo<ProductComponentDomain>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}