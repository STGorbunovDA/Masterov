using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.DeleteOrder.Command;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;

namespace Masterov.Domain.Masterov.Order.DeleteOrder;

public class DeleteOrderUseCase(
    IValidator<DeleteOrderCommand> validator,
    IDeleteOrderStorage storage,
    IGetOrderByIdStorage getOrderByIdStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IGetUsedComponentsByOrderIdStorage getUsedComponentsByOrderIdStorage) : IDeleteOrderUseCase
{
    public async Task<bool> Execute(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var order = await getOrderByIdStorage.GetOrderById(command.OrderId, cancellationToken);
        if (order is null)
            throw new NotFoundByIdException(command.OrderId, "Заказ");

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(command.OrderId, cancellationToken);

        if (payments?.Any() == true)
            throw new Conflict409Exception("Нельзя удалить ордер — у него есть оплата.");
        
        var components = await getUsedComponentsByOrderIdStorage.GetUsedComponentsByOrderId(command.OrderId, cancellationToken);

        if (components?.Any() == true)
            throw new Conflict409Exception("Нельзя удалить ордер — у него есть используемые компоненты.");

        return await storage.DeleteOrder(command.OrderId, cancellationToken);
    }
}