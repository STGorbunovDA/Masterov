using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId.Query;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;

public class GetCustomerByPaymentIdUseCase(IValidator<GetCustomerByPaymentIdQuery> validator, IGetCustomerByPaymentIdStorage storage, IGetPaymentByIdStorage getPaymentByIdStorage) : IGetCustomerByPaymentIdUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByPaymentIdQuery getCustomerByPaymentIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByPaymentIdQuery, cancellationToken);
        var paymentExists = await getPaymentByIdStorage.GetPaymentById(getCustomerByPaymentIdQuery.PaymentId, cancellationToken);
        
        if (paymentExists is null)
            throw new NotFoundByIdException(getCustomerByPaymentIdQuery.PaymentId, "Платеж");
        
        return await storage.GetCustomerByPaymentId(getCustomerByPaymentIdQuery.PaymentId, cancellationToken);
    }
}