using Masterov.Domain.Masterov.Payment.AddPayment.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.AddPayment;

public interface IAddPaymentUseCase
{
    Task<PaymentDomain?> Execute(AddPaymentCommand addPaymentCommand, CancellationToken cancellationToken);
}