using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder.Command;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

namespace Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder;

public class DeleteProductionOrderUseCase(
    IValidator<DeleteProductionOrderCommand> validator,
    IDeleteProductionOrderStorage storage,
    IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IGetProductComponentByOrderIdStorage getProductComponentByOrderIdStorage) : IDeleteProductionOrderUseCase
{
    public async Task<bool> Execute(DeleteProductionOrderCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var productionOrder = await getProductionOrderByOrderIdStorage.GetProductionOrderById(command.ProductionOrderId, cancellationToken);
        if (productionOrder is null)
            throw new NotFoundByIdException(command.ProductionOrderId, "Заказ");

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(command.ProductionOrderId, cancellationToken);

        if (payments?.Any() == true)
            throw new Conflict409Exception("Нельзя удалить ордер — у него есть оплата.");
        
        var productComponents = await getProductComponentByOrderIdStorage.GetProductComponentAtOrder(command.ProductionOrderId, cancellationToken);

        if (productComponents?.Any() == true)
            throw new Conflict409Exception("Нельзя удалить ордер — у него есть используемые компоненты.");

        return await storage.DeleteProductionOrder(command.ProductionOrderId, cancellationToken);
    }
}