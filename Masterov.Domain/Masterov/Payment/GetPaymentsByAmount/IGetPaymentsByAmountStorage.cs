using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;

public interface IGetPaymentsByAmountStorage
{
    Task<IEnumerable<PaymentDomain?>> GetPaymentsByAmount(decimal amount, CancellationToken cancellationToken);
}