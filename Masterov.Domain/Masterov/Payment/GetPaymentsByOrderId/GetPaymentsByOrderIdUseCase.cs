using FluentValidation;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;

public class GetPaymentsByOrderIdUseCase(IValidator<GetPaymentsByOrderIdQuery> validator,
    IGetPaymentsByOrderIdStorage storage) 
    : IGetPaymentsByOrderIdUseCase
{
    public async Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByOrderIdQuery paymentsByOrderIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(paymentsByOrderIdQuery, cancellationToken);
        return await storage.GetPaymentsByOrderId(paymentsByOrderIdQuery.OrderId, cancellationToken);
    }
}