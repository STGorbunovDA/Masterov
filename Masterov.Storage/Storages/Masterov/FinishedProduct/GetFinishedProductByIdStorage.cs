using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductByIdStorage
{
    public async Task<FinishedProductDomain?> GetFinishedProductById(Guid productId, CancellationToken cancellationToken) =>
        await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.FinishedProductId == productId)
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}