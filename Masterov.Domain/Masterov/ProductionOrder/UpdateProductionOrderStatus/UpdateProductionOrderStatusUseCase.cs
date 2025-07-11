using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

public class UpdateProductionOrderStatusUseCase(IValidator<UpdateProductionOrderStatusCommand> validator, 
    IUpdateProductionOrderStatusStorage storage,
    IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage) : IUpdateProductionOrderStatusUseCase
{
    public async Task<ProductionOrderDomain> Execute(UpdateProductionOrderStatusCommand statusCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(statusCommand, cancellationToken);
        
        var productionOrderExists = await getProductionOrderByOrderIdStorage.GetProductionOrderById(statusCommand.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(statusCommand.OrderId, "Ордер (заказ)");
        
        return await storage.UpdateProductionOrderStatus(statusCommand.OrderId, statusCommand.Status, cancellationToken);
    }

}