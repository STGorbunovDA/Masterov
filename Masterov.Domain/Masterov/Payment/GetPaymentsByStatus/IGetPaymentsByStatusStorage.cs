using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;

public interface IGetPaymentsByStatusStorage
{
    Task<IEnumerable<PaymentDomain>?> GetPaymentsByStatus(PaymentMethod paymentMethod, CancellationToken cancellationToken);
}