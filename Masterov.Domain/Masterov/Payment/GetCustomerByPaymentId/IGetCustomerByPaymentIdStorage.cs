using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;

public interface IGetCustomerByPaymentIdStorage
{
    Task<CustomerDomain?> GetCustomerByPaymentId(Guid paymentId, CancellationToken cancellationToken);
}