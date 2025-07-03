using FluentValidation;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;

public class GetPaymentsByPaymentDateUseCase(IValidator<GetPaymentsByPaymentDateQuery> validator,
    IGetPaymentsByPaymentDateStorage storage) : IGetPaymentsByPaymentDateUseCase
{
    public async Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByPaymentDateQuery getPaymentsByPaymentDateQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getPaymentsByPaymentDateQuery, cancellationToken);
        
        return await storage.GetPaymentsByPaymentDate(getPaymentsByPaymentDateQuery.PaymentDate, cancellationToken);
    }
}