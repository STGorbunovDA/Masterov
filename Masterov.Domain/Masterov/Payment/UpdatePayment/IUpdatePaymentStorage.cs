using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.UpdatePayment;

public interface IUpdatePaymentStorage
{
    Task<PaymentDomain> UpdatePayment(Guid paymentId, Guid orderId, Guid customerId, PaymentMethod methodPayment,
        decimal amount, DateTime? createdAt, CancellationToken cancellationToken);
}