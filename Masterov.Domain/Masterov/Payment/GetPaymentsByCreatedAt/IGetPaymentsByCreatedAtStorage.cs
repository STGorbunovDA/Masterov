using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt;

public interface IGetPaymentsByCreatedAtStorage
{
    Task<IEnumerable<PaymentDomain>?> GetPaymentsByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}