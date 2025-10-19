using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.UpdateOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrder;

public class UpdateOrderUseCase(
    IValidator<UpdateOrderCommand> validator,
    IUpdateOrderStorage storage,
    IGetOrderByIdStorage getOrderByIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage,
    IGetFinishedProductByIdStorage getFinishedProductByIdStorage) : IUpdateOrderUseCase
{
    public async Task<OrderDomain> Execute(UpdateOrderCommand updateOrderCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateOrderCommand, cancellationToken);

        var orderExists =
            await getOrderByIdStorage.GetOrderById(updateOrderCommand.OrderId,
                cancellationToken);

        if (orderExists is null)
            throw new NotFoundByIdException(updateOrderCommand.OrderId, "Заказ)");

        var customerExists =
            await getCustomerByIdStorage.GetCustomerById(updateOrderCommand.CustomerId, cancellationToken);

        if (customerExists is null)
            throw new NotFoundByIdException(updateOrderCommand.CustomerId, "Заказчик");
        
        var finishedProductExists =
            await getFinishedProductByIdStorage.GetFinishedProductById(updateOrderCommand.FinishedProductId, cancellationToken);

        if (finishedProductExists is null)
            throw new NotFoundByIdException(updateOrderCommand.FinishedProductId, "Готовое мебельное изделие");

        return await storage.UpdateOrder(
            updateOrderCommand.OrderId,
            updateOrderCommand.CreatedAt, 
            updateOrderCommand.CompletedAt,
            updateOrderCommand.Status,
            updateOrderCommand.Description,
            updateOrderCommand.CustomerId,
            updateOrderCommand.FinishedProductId,
            cancellationToken);
    }
}