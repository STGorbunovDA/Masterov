using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Payment.AddPayment;
using Masterov.Domain.Masterov.Payment.AddPayment.Command;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.ServiceAdditional.ServicePayment;
using Masterov.Domain.Models;

public class AddPaymentUseCase(
    IValidator<AddPaymentCommand> validator,
    IAddPaymentStorage addPaymentStorage,
    IGetOrderByIdStorage getGetOrderByIdStorage,
    IGetCustomerByIdStorage getCustomerByIdStorage,
    IGetPaymentByIdStorage getPaymentByIdStorage,
    IUpdateOrderStatusAfterPayment updateOrderStatusAfterPayment)
    : IAddPaymentUseCase
{
    public async Task<PaymentDomain?> Execute(AddPaymentCommand addPaymentCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addPaymentCommand, cancellationToken);

        var order = await getGetOrderByIdStorage.GetOrderById(addPaymentCommand.OrderId, cancellationToken)
                    ?? throw new NotFoundByIdException(addPaymentCommand.OrderId, "Заказ");

        var customer = await getCustomerByIdStorage.GetCustomerById(addPaymentCommand.CustomerId, cancellationToken)
                       ?? throw new NotFoundByIdException(addPaymentCommand.CustomerId, "Заказчик");

        var payment = await addPaymentStorage.AddPayment(
            addPaymentCommand.OrderId,
            addPaymentCommand.PaymentMethod,
            addPaymentCommand.Amount,
            customer.CustomerId,
            cancellationToken);

        await updateOrderStatusAfterPayment.UpdateOrderStatus(order.OrderId, cancellationToken);

        return await getPaymentByIdStorage.GetPaymentById(payment.PaymentId, cancellationToken);
    }
}