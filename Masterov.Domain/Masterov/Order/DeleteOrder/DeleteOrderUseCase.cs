using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.DeleteOrder.Command;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;

namespace Masterov.Domain.Masterov.Order.DeleteOrder;

public class DeleteOrderUseCase(
    IValidator<DeleteOrderCommand> validator,
    IDeleteOrderStorage storage,
    IGetOrderByIdStorage getOrderByIdStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IGetProductComponentByOrderIdStorage getProductComponentByOrderIdStorage) : IDeleteOrderUseCase
{
    public async Task<bool> Execute(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var productionOrder = await getOrderByIdStorage.GetOrderById(command.OrderId, cancellationToken);
        if (productionOrder is null)
            throw new NotFoundByIdException(command.OrderId, "Заказ");

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(command.OrderId, cancellationToken);

        if (payments?.Any() == true)
            throw new Conflict409Exception("Нельзя удалить ордер — у него есть оплата.");
        
        var productComponents = await getProductComponentByOrderIdStorage.GetProductComponentByOrderId(command.OrderId, cancellationToken);

        if (productComponents?.Any() == true)
            throw new Conflict409Exception("Нельзя удалить ордер — у него есть используемые компоненты.");

        return await storage.DeleteOrder(command.OrderId, cancellationToken);
    }
}