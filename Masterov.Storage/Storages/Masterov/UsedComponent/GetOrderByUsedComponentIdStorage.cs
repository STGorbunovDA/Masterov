using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

public class GetOrderByUsedComponentIdStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetOrderByUsedComponentIdStorage
{
    public async Task<OrderDomain?> GetOrderByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken)
    {
        var usedComponent = await dbContext.UsedComponents
            .AsNoTracking()
                .Include(uc => uc.Order)
                    .ThenInclude(o => o.Customer)
                .Include(uc => uc.Order)
                    .ThenInclude(o => o.Payments)
                .Include(uc => uc.Order)
                    .ThenInclude(o => o.FinishedProduct)
            .FirstOrDefaultAsync(uc => uc.UsedComponentId == usedComponentId, cancellationToken);

        return usedComponent?.Order == null
            ? null
            : mapper.Map<OrderDomain>(usedComponent.Order);
    }
}