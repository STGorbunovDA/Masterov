using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetOrderByPaymentId;

public interface IGetOrderByPaymentIdStorage
{
    Task<OrderDomain?> GetOrderByPaymentId(Guid paymentId, CancellationToken cancellationToken);
}