using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder;

public class UpdateProductionOrderUseCase(
    IValidator<UpdateProductionOrderCommand> validator,
    IUpdateProductionOrderStorage storage,
    IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage) : IUpdateProductionOrderUseCase
{
    public async Task<OrderDomain> Execute(UpdateProductionOrderCommand updateProductionOrderCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateProductionOrderCommand, cancellationToken);

        var productionOrderExists =
            await getProductionOrderByOrderIdStorage.GetProductionOrderById(updateProductionOrderCommand.OrderId,
                cancellationToken);

        if (productionOrderExists is null)
            throw new NotFoundByIdException(updateProductionOrderCommand.OrderId, "Ордер (заказ)");

        var customerExists =
            await getCustomerByIdStorage.GetCustomerById(updateProductionOrderCommand.CustomerId, cancellationToken);

        if (customerExists is null)
            throw new NotFoundByIdException(updateProductionOrderCommand.CustomerId, "Заказчик");

        return await storage.UpdateProductionOrder(
            updateProductionOrderCommand.OrderId,
            updateProductionOrderCommand.CreatedAt, 
            updateProductionOrderCommand.Status,
            updateProductionOrderCommand.Description,
            updateProductionOrderCommand.CustomerId, cancellationToken);
    }
}