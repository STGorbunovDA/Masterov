using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductsByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByCreatedAtStorage
{
    public async Task<IEnumerable<FinishedProductDomain>?> GetFinishedProductsByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var customers = await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.Components)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.Payments)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<FinishedProductDomain>>(customers);
    }
}