using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class AddUsedComponentStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddUsedComponentStorage
{
    public async Task<UsedComponentDomain> AddUsedComponent(Guid orderId, Guid productTypeId, Guid warehouseId, int quantity, CancellationToken cancellationToken)
    {
        var usedComponentIdGuide = guidFactory.Create();
        var usedComponent = new Storage.UsedComponent()
        {
            UsedComponentId = usedComponentIdGuide,
            OrderId = orderId,
            ProductTypeId = productTypeId,
            WarehouseId = warehouseId,
            Quantity = quantity,
            CreatedAt = DateTime.Now
        };

        await dbContext.UsedComponents.AddAsync(usedComponent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.UsedComponents
            .Where(t => t.UsedComponentId == usedComponentIdGuide)
            .ProjectTo<UsedComponentDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}