using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;

public interface IGetPaymentsByOrderIdUseCase
{
    Task<IEnumerable<PaymentDomain>?> Execute(GetPaymentsByOrderIdQuery getPaymentsByOrderIdQuery, CancellationToken cancellationToken);
}