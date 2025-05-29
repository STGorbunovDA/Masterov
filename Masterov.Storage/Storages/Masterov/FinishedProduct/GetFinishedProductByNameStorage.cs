using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetFinishedProductByNameStorage
{
    public async Task<FinishedProductDomain?> GetFinishedProductByName(string finishedProductName, CancellationToken cancellationToken)=>
        (await memoryCache.GetOrCreateAsync<FinishedProductDomain?>( 
            nameof(GetFinishedProductByName),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.FinishedProducts
                    .Where(f => f.Name.ToLower() == finishedProductName.ToLower().Trim())
                    .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}