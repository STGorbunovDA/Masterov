using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentById;

public interface IGetPaymentByIdStorage
{
    Task<PaymentDomain?> GetPaymentById(Guid paymentId, CancellationToken cancellationToken);
}