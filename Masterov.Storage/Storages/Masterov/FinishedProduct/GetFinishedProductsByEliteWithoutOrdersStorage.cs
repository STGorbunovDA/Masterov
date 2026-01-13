using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByEliteWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsByEliteWithoutOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByEliteWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductsByEliteWithoutOrders(CancellationToken cancellationToken)
    {
        return await dbContext.FinishedProducts
            .AsNoTracking()
            .Where(x => x.Elite)
            .ProjectTo<FinishedProductNoOrdersDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}