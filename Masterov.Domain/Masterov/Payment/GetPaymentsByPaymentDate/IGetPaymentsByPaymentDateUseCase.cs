using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;

public interface IGetPaymentsByPaymentDateUseCase
{
    Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByPaymentDateQuery getPaymentsByPaymentDateQuery, CancellationToken cancellationToken);
}