using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPayments;

public interface IGetPaymentsStorage
{
    Task<IEnumerable<PaymentDomain>> GetPayments(CancellationToken cancellationToken);
}