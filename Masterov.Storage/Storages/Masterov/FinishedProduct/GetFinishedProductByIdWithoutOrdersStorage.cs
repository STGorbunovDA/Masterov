using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByIdWithoutOrdersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductByIdWithoutOrdersStorage
{
    public async Task<FinishedProductNoOrdersDomain?> GetFinishedProductByIdWithoutOrders(Guid productId, CancellationToken cancellationToken) =>
        await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.FinishedProductId == productId)
            .ProjectTo<FinishedProductNoOrdersDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}