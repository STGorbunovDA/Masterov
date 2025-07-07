using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.AddPayment;

public interface IAddPaymentStorage
{
    Task<PaymentDomain> AddPayment(Guid orderId, PaymentMethod paymentMethod, decimal amount, Guid customerId, CancellationToken cancellationToken);
}