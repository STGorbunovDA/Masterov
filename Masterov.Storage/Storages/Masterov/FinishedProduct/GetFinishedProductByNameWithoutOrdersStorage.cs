using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductByNameWithoutOrdersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductByNameWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductByNameWithoutOrders(string finishedProductName, CancellationToken cancellationToken)
    {
        var normalizedName = finishedProductName.Trim().ToLower();
    
        return await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Name.ToLower() == normalizedName)
            .ProjectTo<FinishedProductNoOrdersDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}