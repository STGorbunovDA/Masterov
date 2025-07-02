using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;

public interface IGetPaymentsByStatusUseCase
{
    Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByStatusQuery getPaymentsByStatusQuery, CancellationToken cancellationToken);
}