using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByElite;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsByEliteStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByEliteStorage
{
    public async Task<IEnumerable<FinishedProductDomain?>> GetFinishedProductsByElite(
        CancellationToken cancellationToken) =>
        await dbContext.FinishedProducts
            .AsNoTracking()
            .Where(x => x.Elite)
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}