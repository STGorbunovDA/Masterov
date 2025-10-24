using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductsByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByUpdatedAtStorage
{
    public async Task<IEnumerable<FinishedProductDomain>?> GetFinishedProductsByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var customers = await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.UsedComponents)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.Payments)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<FinishedProductDomain>>(customers);
    }
}