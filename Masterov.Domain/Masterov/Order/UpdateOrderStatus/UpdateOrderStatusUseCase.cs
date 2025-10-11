using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrderStatus;

public class UpdateOrderStatusUseCase(IValidator<UpdateOrderStatusCommand> validator, IUpdateOrderStatusStorage storage,
    IGetOrderByIdStorage getOrderByIdStorage) : IUpdateOrderStatusUseCase
{
    public async Task<OrderDomain> Execute(UpdateOrderStatusCommand statusCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(statusCommand, cancellationToken);
        
        var orderExists = await getOrderByIdStorage.GetOrderById(statusCommand.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(statusCommand.OrderId, "Заказ)");
        
        return await storage.UpdateOrderStatus(statusCommand.OrderId, statusCommand.OrderStatus, cancellationToken);
    }

}