using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;

public interface IGetPaymentsByPaymentDateStorage
{
    Task<IEnumerable<PaymentDomain>?> GetPaymentsByPaymentDate(DateTime paymentDate, CancellationToken cancellationToken);
}