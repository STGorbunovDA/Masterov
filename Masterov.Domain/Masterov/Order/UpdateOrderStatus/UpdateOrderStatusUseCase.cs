using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrderStatus;

public class UpdateOrderStatusUseCase(IValidator<UpdateOrderStatusCommand> validator, 
    IUpdateOrderStatusStorage storage,
    IGetOrderByOrderIdStorage getOrderByOrderIdStorage) : IUpdateOrderStatusUseCase
{
    public async Task<OrderDomain> Execute(UpdateOrderStatusCommand statusCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(statusCommand, cancellationToken);
        
        var productionOrderExists = await getOrderByOrderIdStorage.GetOrderByOrderId(statusCommand.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(statusCommand.OrderId, "Ордер (заказ)");
        
        return await storage.UpdateOrderStatus(statusCommand.OrderId, statusCommand.Status, cancellationToken);
    }

}