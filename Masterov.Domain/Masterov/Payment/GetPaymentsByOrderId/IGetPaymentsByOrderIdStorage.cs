using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;

public interface IGetPaymentsByOrderIdStorage
{
    Task<IEnumerable<PaymentDomain>?> GetPaymentsByOrderId(Guid orderId, CancellationToken cancellationToken);
}