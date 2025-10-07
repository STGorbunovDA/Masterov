using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetOrderByPaymentId.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetOrderByPaymentId;

public class GetOrderByPaymentIdUseCase(IValidator<GetOrderByPaymentIdQuery> validator, IGetOrderByPaymentIdStorage storage, IGetPaymentByIdStorage getPaymentByIdStorage) : IGetOrderByPaymentIdUseCase
{
    public async Task<OrderDomain?> Execute(GetOrderByPaymentIdQuery getOrderByPaymentIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrderByPaymentIdQuery, cancellationToken);
        var paymentExists = await getPaymentByIdStorage.GetPaymentById(getOrderByPaymentIdQuery.PaymentId, cancellationToken);
        
        if (paymentExists is null)
            throw new NotFoundByIdException(getOrderByPaymentIdQuery.PaymentId, "Платеж");
        
        return await storage.GetOrderByPaymentId(getOrderByPaymentIdQuery.PaymentId, cancellationToken);
    }
}