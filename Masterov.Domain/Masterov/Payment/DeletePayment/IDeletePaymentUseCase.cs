using Masterov.Domain.Masterov.Payment.DeletePayment.Command;

namespace Masterov.Domain.Masterov.Payment.DeletePayment;

public interface IDeletePaymentUseCase
{
    Task<bool> Execute(DeletePaymentCommand deletePaymentCommand, CancellationToken cancellationToken);
}