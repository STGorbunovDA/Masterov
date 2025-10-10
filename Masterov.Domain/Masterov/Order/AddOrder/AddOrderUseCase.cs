using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.Order.AddOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.AddOrder;

public class AddOrderUseCase(
    IValidator<AddOrderCommand> validator,
    IAddOrderStorage storage,
    IGetCustomerByIdStorage getCustomerByIdStorage,
    IGetFinishedProductByIdStorage getFinishedProductByIdStorage) : IAddOrderUseCase
{
    public async Task<OrderDomain> Execute(AddOrderCommand addOrderCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addOrderCommand, cancellationToken);
        
        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(addOrderCommand.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(addOrderCommand.FinishedProductId, "Готовое мебельное изделие");
        
        var customerDomain = await getCustomerByIdStorage.GetCustomerById(addOrderCommand.CustomerId, cancellationToken);
        
        if (customerDomain is null)
            throw new NotFoundByIdException(addOrderCommand.CustomerId, "Заказчик");

        return await storage.AddOrder(addOrderCommand.FinishedProductId, customerDomain.CustomerId,
            addOrderCommand.Description, cancellationToken);

    }
    
}