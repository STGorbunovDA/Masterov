using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductsByCreatedAtWithoutOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByCreatedAtWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain>?> GetFinishedProductsByCreatedAtWithoutOrders(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var customers = await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<FinishedProductNoOrdersDomain>>(customers);
    }
}