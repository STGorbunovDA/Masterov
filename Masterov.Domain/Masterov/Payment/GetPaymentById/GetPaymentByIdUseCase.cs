using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetPaymentById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentById;

public class GetPaymentByIdUseCase(IValidator<GetPaymentByIdQuery> validator, IGetPaymentByIdStorage storage) : IGetPaymentByIdUseCase
{
    public async Task<PaymentDomain?> Execute(GetPaymentByIdQuery getPaymentByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getPaymentByIdQuery, cancellationToken);
        var paymentExists = await storage.GetPaymentById(getPaymentByIdQuery.PaymentId, cancellationToken);
        
        if (paymentExists is null)
            throw new NotFoundByIdException(getPaymentByIdQuery.PaymentId, "Платеж");
        
        return paymentExists;
    }
}