using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByTypeStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductByTypeStorage
{
    public async Task<IEnumerable<FinishedProductDomain?>> GetFinishedProductByType(string finishedProductType, CancellationToken cancellationToken)
    {
        var normalizedType = finishedProductType.Trim().ToLower();
    
        return await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Type.ToLower() == normalizedType)
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}