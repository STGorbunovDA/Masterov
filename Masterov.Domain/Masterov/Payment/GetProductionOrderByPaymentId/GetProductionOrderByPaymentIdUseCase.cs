using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;

public class GetProductionOrderByPaymentIdUseCase(IValidator<GetProductionOrderByPaymentIdQuery> validator, IGetProductionOrderByPaymentIdStorage storage, IGetPaymentByIdStorage getPaymentByIdStorage) : IGetProductionOrderByPaymentIdUseCase
{
    public async Task<ProductionOrderDomain?> Execute(GetProductionOrderByPaymentIdQuery getProductionOrderByPaymentIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductionOrderByPaymentIdQuery, cancellationToken);
        var productionOrderExists = await getPaymentByIdStorage.GetPaymentById(getProductionOrderByPaymentIdQuery.PaymentId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getProductionOrderByPaymentIdQuery.PaymentId, "Платеж");
        
        return await storage.GetProductionOrderByPaymentId(getProductionOrderByPaymentIdQuery.PaymentId, cancellationToken);
    }
}