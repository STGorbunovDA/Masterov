using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPayments;

public class GetPaymentsUseCase(IGetPaymentsStorage storage) : IGetPaymentsUseCase
{
    public async Task<IEnumerable<PaymentDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetPayments(cancellationToken);

}