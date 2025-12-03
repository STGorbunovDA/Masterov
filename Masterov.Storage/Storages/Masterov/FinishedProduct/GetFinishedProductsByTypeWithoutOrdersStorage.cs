using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsByTypeWithoutOrdersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByTypeWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductByTypeWithoutOrders(string finishedProductType, CancellationToken cancellationToken)
    {
        var normalizedName = finishedProductType.Trim().ToLower();
    
        return await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Type.ToLower() == normalizedName)
            .ProjectTo<FinishedProductNoOrdersDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}