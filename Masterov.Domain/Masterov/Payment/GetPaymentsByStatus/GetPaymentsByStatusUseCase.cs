using FluentValidation;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;

public class GetPaymentsByStatusUseCase(IValidator<GetPaymentsByStatusQuery> validator,
    IGetPaymentsByStatusStorage storage) 
    : IGetPaymentsByStatusUseCase
{
    public async Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByStatusQuery getPaymentsByStatusQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getPaymentsByStatusQuery, cancellationToken);
        
        return await storage.GetPaymentsByStatus(getPaymentsByStatusQuery.PaymentMethod, cancellationToken);
    }
}