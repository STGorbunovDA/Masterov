using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;

public interface IGetProductionOrderByPaymentIdUseCase
{
    Task<ProductionOrderDomain?> Execute(GetProductionOrderByPaymentIdQuery getProductionOrderByPaymentIdQuery, CancellationToken cancellationToken);
}