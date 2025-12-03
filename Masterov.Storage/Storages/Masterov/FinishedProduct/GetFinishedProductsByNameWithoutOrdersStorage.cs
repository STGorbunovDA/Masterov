using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsByNameWithoutOrdersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByNameWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductByNameWithoutOrders(string finishedProductName, CancellationToken cancellationToken)
    {
        var normalizedName = finishedProductName.Trim().ToLower();
    
        return await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Name.ToLower().Contains(normalizedName))
            .ProjectTo<FinishedProductNoOrdersDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}