using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Payment.AddPayment.Command;
using Masterov.Domain.Masterov.Payment.DeletePayment;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.AddPayment;

public class AddPaymentUseCase(
    IValidator<AddPaymentCommand> validator,
    IAddPaymentStorage addPaymentStorage,
    IGetProductionOrderByIdStorage getProductionOrderByIdStorage,
    IGetCustomerByPhoneStorage getCustomerByPhoneStorage,
    IGetCustomerByEmailStorage getCustomerByEmailStorage,
    IGetCustomerByNameStorage getCustomerByNameStorage,
    IAddCustomerStorage addCustomerStorage,
    IUpdateProductionOrderStatusStorage updateProductionOrderStatusStorage,
    IGetFinishedProductAtOrderStorage getFinishedProductAtOrderStorage,
    IGetPaymentsByOrderIdStorage getPaymentsByOrderIdStorage,
    IGetPaymentByIdStorage getPaymentByIdStorage,
    IDeletePaymentStorage deletePaymentStorage) : IAddPaymentUseCase
{
    public async Task<PaymentDomain?> Execute(AddPaymentCommand command, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(command, ct); // валидация
        
        var order = await EnsureOrderExists(command.OrderId, ct); // проверка наличия заказа

        var customer = await GetOrCreateCustomer(command, ct); // получаем заказчика

        var payment = await addPaymentStorage.AddPayment(command.OrderId,
            command.PaymentMethod, command.Amount, customer.CustomerId, ct); // добавляем платеж

        var finishProduct = await getFinishedProductAtOrderStorage.GetFinishedProductAtOrder(command.OrderId, ct); // получаем готовый продукт 

        var payments = await getPaymentsByOrderIdStorage.GetPaymentsByOrderId(command.OrderId, ct); // получаем все платежи по ордеру (заказу)
        var totalPaid = payments?.Sum(p => p.Amount) ?? 0; // получаем общую сумму по платежам

        // метод изменения статуса заказа путем сравнения цены готового мебельного изделия с ценой всех платежей
        await UpdateOrderStatusIfNeeded(order.OrderId, finishProduct?.Price ?? 0, totalPaid, ct);
        
        return await getPaymentByIdStorage.GetPaymentById(payment.PaymentId, ct);
    }
    
    
    private async Task<ProductionOrderDomain> EnsureOrderExists(Guid orderId, CancellationToken ct)
    {
        var order = await getProductionOrderByIdStorage.GetProductionOrderById(orderId, ct);
        if (order is null)
            throw new NotFoundByIdException(orderId, "Ордер (заказ)");
        return order;
    }

    private async Task<CustomerDomain> GetOrCreateCustomer(AddPaymentCommand cmd, CancellationToken ct)
    {
        CustomerDomain? customer = null;

        if (cmd.PhoneCustomer is not null)
            customer = await getCustomerByPhoneStorage.GetCustomerByPhone(cmd.PhoneCustomer, ct); // смотрим по номеру телефона

        if (customer is null && cmd.EmailCustomer is not null)
            customer = await getCustomerByEmailStorage.GetCustomerByEmail(cmd.EmailCustomer, ct); // смотрим по почте

        // if (customer is null) // TODO имена могут быть одинаковы при добавлении
        //     customer = await getCustomerByNameStorage.GetCustomerByName(cmd.NameCustomer, ct); // смотрим по имени

        if (customer is null)
            customer = await addCustomerStorage.AddCustomer(cmd.NameCustomer, cmd.EmailCustomer, cmd.PhoneCustomer, ct);

        return customer ?? throw new InvalidOperationException("Не удалось создать или найти заказчика для платежа");
    }

    private async Task UpdateOrderStatusIfNeeded(Guid orderId, decimal productPrice, decimal paidTotal, CancellationToken ct)
    {
        if (productPrice <= 0) // если цены готового мебельного изделия нет
            return;
        
        // если общая цена готового мебельного изделия будет меньше или равна общей цены всех платежей данного ордера
        // тогда необходимо присвоить статус InProgress (в работу) иначе Partial (предоплата или неполная оплата)
        var status = productPrice <= paidTotal 
            ? ProductionOrderStatus.InProgress
            : ProductionOrderStatus.Partial;

        var updatedOrder = await updateProductionOrderStatusStorage.UpdateProductionOrderStatus(orderId, status, ct); // метод изменения статуса

        if (updatedOrder is null)
            throw new InvalidOperationException("Не удалось обновить статус заказа после оплаты");
    }
}