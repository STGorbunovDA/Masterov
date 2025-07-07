using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

public class UpdateProductionOrderStatusUseCase(IValidator<UpdateProductionOrderStatusCommand> validator, 
    IUpdateProductionOrderStatusStorage storage) : IUpdateProductionOrderStatusUseCase
{
    public async Task<ProductionOrderDomain> Execute(UpdateProductionOrderStatusCommand statusCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(statusCommand, cancellationToken);
        
        return await storage.UpdateProductionOrderStatus(statusCommand.OrderId, statusCommand.Status, cancellationToken);
    }

}