﻿using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;

public class GetPaymentsByAmountUseCase(IValidator<GetPaymentsByAmountQuery> validator, IGetPaymentsByAmountStorage storage) : IGetPaymentsByAmountUseCase
{
    public async Task<IEnumerable<PaymentDomain?>> Execute(GetPaymentsByAmountQuery getPaymentsByAmountQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getPaymentsByAmountQuery, cancellationToken);
        var paymentExists = await storage.GetPaymentsByAmount(getPaymentsByAmountQuery.Amount, cancellationToken);
        
        if (paymentExists is null)
            throw new NotFoundByAmountException(getPaymentsByAmountQuery.Amount, "Платеж");
        
        return paymentExists;
    }
}