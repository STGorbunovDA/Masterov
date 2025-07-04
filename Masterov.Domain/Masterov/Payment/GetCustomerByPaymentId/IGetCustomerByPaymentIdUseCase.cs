using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;

public interface IGetCustomerByPaymentIdUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByPaymentIdQuery getCustomerByPaymentIdQuery, CancellationToken cancellationToken);
}