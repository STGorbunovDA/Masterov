using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.UpdatePayment.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.UpdatePayment;

public class UpdatePaymentUseCase(
    IValidator<UpdatePaymentCommand> validator,
    IUpdatePaymentStorage updatePaymentStorage,
    IGetPaymentByIdStorage getPaymentByIdStorage) : IUpdatePaymentUseCase
{
    public async Task<PaymentDomain> Execute(UpdatePaymentCommand updatePaymentCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updatePaymentCommand, cancellationToken);

        var paymentExists =
            await getPaymentByIdStorage.GetPaymentById(updatePaymentCommand.PaymentId, cancellationToken);

        if (paymentExists is null)
            throw new NotFoundByIdException(updatePaymentCommand.PaymentId, "Платеж");

        return await updatePaymentStorage.UpdatePayment(updatePaymentCommand.PaymentId, updatePaymentCommand.OrderId,
            updatePaymentCommand.CustomerId, updatePaymentCommand.MethodPayment, updatePaymentCommand.Amount,
            updatePaymentCommand.PaymentDate, cancellationToken);
    }
}