namespace Masterov.Domain.Masterov.Payment.DeletePayment;

public interface IDeletePaymentStorage
{
    Task<bool> DeletePayment(Guid PaymentId, CancellationToken cancellationToken);
}