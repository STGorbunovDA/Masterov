using Masterov.Domain.Masterov.Payment.UpdatePayment.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.UpdatePayment;

public interface IUpdatePaymentUseCase
{
    Task<PaymentDomain?> Execute(UpdatePaymentCommand command, CancellationToken cancellationToken);
}