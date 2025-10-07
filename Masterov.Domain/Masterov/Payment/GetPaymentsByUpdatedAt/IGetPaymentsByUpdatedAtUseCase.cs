using Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt;

public interface IGetPaymentsByUpdatedAtUseCase
{
    Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByUpdatedAtQuery getPaymentsByUpdatedAtQuery, CancellationToken cancellationToken);
}