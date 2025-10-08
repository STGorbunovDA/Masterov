using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductsByUpdatedAtWithoutOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByUpdatedAtWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductWithoutOrdersDomain>?> GetFinishedProductsByUpdatedAtWithoutOrders(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var customers = await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<FinishedProductWithoutOrdersDomain>>(customers);
    }
}