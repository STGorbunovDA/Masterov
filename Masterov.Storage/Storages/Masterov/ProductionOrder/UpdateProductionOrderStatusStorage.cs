using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class UpdateProductionOrderStatusStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateProductionOrderStatusStorage
{
    public async Task<ProductionOrderDomain> UpdateProductionOrderStatus(Guid orderId, ProductionOrderStatus status, CancellationToken cancellationToken)
    {
        var productionOrder = await dbContext.Set<Storage.ProductionOrder>().FindAsync([orderId], cancellationToken);
        
        if (productionOrder == null)
            throw new Exception("ProductionOrder not found");
        
        productionOrder.Status = status;
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductionOrderDomain>(productionOrder);
    }

    
}