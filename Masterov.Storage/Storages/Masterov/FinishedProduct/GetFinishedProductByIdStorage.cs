using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetFinishedProductByIdStorage
{
    public async Task<FinishedProductDomain?> GetFinishedProductById(Guid productId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<FinishedProductDomain?>( 
            nameof(GetFinishedProductById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.FinishedProducts
                    .AsNoTracking() 
                    .Where(f => f.FinishedProductId == productId)
                    .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}