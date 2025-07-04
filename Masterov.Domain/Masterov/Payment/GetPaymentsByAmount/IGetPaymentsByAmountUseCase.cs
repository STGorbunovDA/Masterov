using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;

public interface IGetPaymentsByAmountUseCase
{
    Task<IEnumerable<PaymentDomain?>> Execute(GetPaymentsByAmountQuery getPaymentsByAmountQuery, CancellationToken cancellationToken);
}