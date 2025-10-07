using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder;

public class AddProductionOrderUseCase(
    IValidator<AddProductionOrderCommand> validator,
    IAddProductionOrderStorage storage,
    IGetCustomerByIdStorage getCustomerByIdStorage,
    IAddCustomerStorage addCustomerStorage,
    IGetFinishedProductByIdStorage getFinishedProductByIdStorage
    ) : IAddProductionOrderUseCase
{
    public async Task<OrderDomain> Execute(AddProductionOrderCommand addProductionOrderCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addProductionOrderCommand, cancellationToken); // валидация
        
        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(addProductionOrderCommand.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(addProductionOrderCommand.FinishedProductId, "Изделие");

        CustomerDomain? customerDomain = null;

        if (addProductionOrderCommand.CustomerId is not null)
        {
            customerDomain = await getCustomerByIdStorage
                .GetCustomerById(addProductionOrderCommand.CustomerId.Value, cancellationToken);
        }
        
        if (customerDomain is null && addProductionOrderCommand.CustomerName is not null &&
            (addProductionOrderCommand.CustomerPhone is not null ||
             addProductionOrderCommand.CustomerEmail is not null))
        {
            customerDomain = await addCustomerStorage.AddCustomer(addProductionOrderCommand.CustomerName,
                addProductionOrderCommand.CustomerPhone, addProductionOrderCommand.CustomerEmail, cancellationToken);
        }
        
        if(customerDomain is null)
            throw new Conflict422Exception("Заказ не возможно добавить т.к. не указано Имя и (Почта или Номер)");


        return await storage.AddProductionOrder(addProductionOrderCommand.FinishedProductId, customerDomain.CustomerId,
            addProductionOrderCommand.Description, cancellationToken);

    }
    
}