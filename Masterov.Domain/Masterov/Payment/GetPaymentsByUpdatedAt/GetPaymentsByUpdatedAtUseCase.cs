using FluentValidation;
using Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt;

public class GetPaymentsByUpdatedAtUseCase(IValidator<GetPaymentsByUpdatedAtQuery> validator,
    IGetPaymentsByUpdatedAtStorage storage) : IGetPaymentsByUpdatedAtUseCase
{
    public async Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByUpdatedAtQuery getPaymentsByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getPaymentsByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetPaymentsByUpdatedAt(getPaymentsByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}