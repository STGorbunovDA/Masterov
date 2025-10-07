using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.UpdatePayment.Command;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.UpdatePayment;

public class UpdatePaymentUseCase(
    IValidator<UpdatePaymentCommand> validator,
    IUpdatePaymentStorage updatePaymentStorage,
    IGetPaymentByIdStorage getPaymentByIdStorage,
    IGetProductionOrderByOrderIdStorage getOrderByOrderIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage, 
    IGetFinishedProductByOrderIdStorage getFinishedProductByOrderIdStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IUpdateProductionOrderStatusStorage updateProductionOrderStatusStorage) : IUpdatePaymentUseCase
{
    public async Task<PaymentDomain?> Execute(UpdatePaymentCommand updatePaymentCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updatePaymentCommand, cancellationToken);

        var paymentExists =
            await getPaymentByIdStorage.GetPaymentById(updatePaymentCommand.PaymentId, cancellationToken);

        if (paymentExists is null)
            throw new NotFoundByIdException(updatePaymentCommand.PaymentId, "Платеж");
        
        var order = await getOrderByOrderIdStorage.GetProductionOrderById(updatePaymentCommand.OrderId, cancellationToken);
        
        if (order is null)
            throw new NotFoundByIdException(updatePaymentCommand.OrderId, "Заказ");
        
        var customer = await getCustomerByIdStorage.GetCustomerById(updatePaymentCommand.CustomerId, cancellationToken);
        
        if (customer is null)
            throw new NotFoundByIdException(updatePaymentCommand.CustomerId, "Заказчик");

        var paymentUpdate = await updatePaymentStorage.UpdatePayment(updatePaymentCommand.PaymentId, updatePaymentCommand.OrderId,
            updatePaymentCommand.CustomerId, updatePaymentCommand.MethodPayment, updatePaymentCommand.Amount,
            updatePaymentCommand.CreatedAt, cancellationToken);
        
        var finishProduct = await getFinishedProductByOrderIdStorage.GetFinishedProductByOrderId(updatePaymentCommand.OrderId, cancellationToken); 

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(updatePaymentCommand.OrderId, cancellationToken);
        var totalPaid = payments?.Sum(p => p.Amount) ?? 0;

        // метод изменения статуса заказа путем сравнения цены готового мебельного изделия с ценой всех платежей
        await UpdateOrderStatusIfNeeded(order.OrderId, finishProduct?.Price ?? 0, totalPaid, cancellationToken);
        
        return await getPaymentByIdStorage.GetPaymentById(paymentUpdate.PaymentId, cancellationToken);
    }
    
    private async Task UpdateOrderStatusIfNeeded(Guid orderId, decimal productPrice, decimal paidTotal, CancellationToken cancellationToken)
    {
        if (productPrice <= 0) // если цены готового мебельного изделия нет
            return;
        
        // если общая цена готового мебельного изделия будет меньше или равна общей цены всех платежей данного ордера
        // тогда необходимо присвоить статус InProgress (в работу) иначе Partial (предоплата или неполная оплата)
        var status = productPrice <= paidTotal 
            ? ProductionOrderStatus.InProgress
            : ProductionOrderStatus.Partial;
        
        var updatedOrder = await updateProductionOrderStatusStorage.UpdateProductionOrderStatus(orderId, status, cancellationToken); // метод изменения статуса
        
        if (updatedOrder is null)
            throw new InvalidOperationException("Не удалось обновить статус заказа после оплаты");
        
    }
}