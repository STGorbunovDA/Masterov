using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt;

public interface IGetPaymentsByUpdatedAtStorage
{
    Task<IEnumerable<PaymentDomain>?> GetPaymentsByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}