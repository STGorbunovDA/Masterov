using Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt;

public interface IGetPaymentsByCreatedAtUseCase
{
    Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByCreatedAtQuery getPaymentsByCreatedAtQuery, CancellationToken cancellationToken);
}