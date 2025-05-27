using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Product.GetProductById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Product;

internal class GetProductByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductByIdStorage
{
    public async Task<ProductDomain?> GetProductById(Guid productId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductDomain?>( 
            nameof(GetProductById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Products
                    .Where(f => f.ProductId == productId)
                    .ProjectTo<ProductDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}