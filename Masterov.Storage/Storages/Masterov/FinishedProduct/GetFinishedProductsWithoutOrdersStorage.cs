using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsWithoutOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain>> GetFinishedProductsWithoutOrders(CancellationToken cancellationToken)
    {
        return await dbContext.FinishedProducts
            .AsNoTracking()
            .ProjectTo<FinishedProductNoOrdersDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}