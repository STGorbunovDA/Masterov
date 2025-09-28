using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsStorage
{
    public async Task<IEnumerable<FinishedProductDomain>> GetFinishedProducts(CancellationToken cancellationToken) =>
        await dbContext.FinishedProducts
            .AsNoTracking() 
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}