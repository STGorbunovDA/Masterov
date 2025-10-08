using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductByNameStorage
{
    public async Task<IEnumerable<FinishedProductDomain?>> GetFinishedProductByName(string finishedProductName, CancellationToken cancellationToken)
    {
        var normalizedName = finishedProductName.Trim().ToLower();
    
        return await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Name.ToLower() == normalizedName)
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}