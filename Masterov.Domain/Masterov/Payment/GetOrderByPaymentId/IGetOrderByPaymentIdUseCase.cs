using Masterov.Domain.Masterov.Payment.GetOrderByPaymentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetOrderByPaymentId;

public interface IGetOrderByPaymentIdUseCase
{
    Task<OrderDomain?> Execute(GetOrderByPaymentIdQuery getOrderByPaymentIdQuery, CancellationToken cancellationToken);
}