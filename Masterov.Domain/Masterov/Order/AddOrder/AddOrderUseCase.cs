using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.Order.AddOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.AddOrder;

public class AddOrderUseCase(
    IValidator<AddOrderCommand> validator,
    IAddOrderStorage storage,
    IGetCustomerByIdStorage getCustomerByIdStorage,
    IAddCustomerStorage addCustomerStorage,
    IGetFinishedProductByIdStorage getFinishedProductByIdStorage
    ) : IAddOrderUseCase
{
    public async Task<OrderDomain> Execute(AddOrderCommand addOrderCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addOrderCommand, cancellationToken); // валидация
        
        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(addOrderCommand.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(addOrderCommand.FinishedProductId, "Изделие");

        CustomerDomain? customerDomain = null;

        if (addOrderCommand.CustomerId is not null)
        {
            customerDomain = await getCustomerByIdStorage
                .GetCustomerById(addOrderCommand.CustomerId.Value, cancellationToken);
        }
        
        if (customerDomain is null && addOrderCommand.CustomerName is not null &&
            (addOrderCommand.CustomerPhone is not null ||
             addOrderCommand.CustomerEmail is not null))
        {
            customerDomain = await addCustomerStorage.AddCustomer(addOrderCommand.CustomerName,
                addOrderCommand.CustomerPhone, addOrderCommand.CustomerEmail, cancellationToken);
        }
        
        if(customerDomain is null)
            throw new Conflict422Exception("Заказ не возможно добавить т.к. не указано Имя и (Почта или Номер)");


        return await storage.AddOrder(addOrderCommand.FinishedProductId, customerDomain.CustomerId,
            addOrderCommand.Description, cancellationToken);

    }
    
}