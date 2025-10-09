using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.UpdateOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrder;

public class UpdateOrderUseCase(
    IValidator<UpdateOrderCommand> validator,
    IUpdateOrderStorage storage,
    IGetOrderByIdStorage getOrderByIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage) : IUpdateOrderUseCase
{
    public async Task<OrderDomain> Execute(UpdateOrderCommand updateOrderCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateOrderCommand, cancellationToken);

        var productionOrderExists =
            await getOrderByIdStorage.GetOrderById(updateOrderCommand.OrderId,
                cancellationToken);

        if (productionOrderExists is null)
            throw new NotFoundByIdException(updateOrderCommand.OrderId, "Ордер (заказ)");

        var customerExists =
            await getCustomerByIdStorage.GetCustomerById(updateOrderCommand.CustomerId, cancellationToken);

        if (customerExists is null)
            throw new NotFoundByIdException(updateOrderCommand.CustomerId, "Заказчик");

        return await storage.UpdateOrder(
            updateOrderCommand.OrderId,
            updateOrderCommand.CreatedAt, 
            updateOrderCommand.CompletedAt,
            updateOrderCommand.Status,
            updateOrderCommand.Description,
            updateOrderCommand.CustomerId, cancellationToken);
    }
}