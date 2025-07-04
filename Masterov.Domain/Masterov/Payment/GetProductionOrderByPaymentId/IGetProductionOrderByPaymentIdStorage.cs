using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;

public interface IGetProductionOrderByPaymentIdStorage
{
    Task<ProductionOrderDomain?> GetProductionOrderByPaymentId(Guid paymentId, CancellationToken cancellationToken);
}