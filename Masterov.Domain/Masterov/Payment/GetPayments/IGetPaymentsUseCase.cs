using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPayments;

public interface IGetPaymentsUseCase
{
    Task<IEnumerable<PaymentDomain?>> Execute(CancellationToken cancellationToken);
}