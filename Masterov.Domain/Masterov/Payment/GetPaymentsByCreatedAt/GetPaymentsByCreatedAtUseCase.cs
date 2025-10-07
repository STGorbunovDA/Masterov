using FluentValidation;
using Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt;

public class GetPaymentsByCreatedAtUseCase(IValidator<GetPaymentsByCreatedAtQuery> validator,
    IGetPaymentsByCreatedAtStorage storage) : IGetPaymentsByCreatedAtUseCase
{
    public async Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByCreatedAtQuery getPaymentsByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getPaymentsByCreatedAtQuery, cancellationToken);
        
        var x = storage.GetPaymentsByCreatedAt(getPaymentsByCreatedAtQuery.CreatedAt, cancellationToken).Result;
        
        return await storage.GetPaymentsByCreatedAt(getPaymentsByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}