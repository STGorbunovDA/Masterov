using Masterov.Domain.Masterov.Payment.GetPaymentById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Payment.GetPaymentById;

public interface IGetPaymentByIdUseCase
{
    Task<PaymentDomain?> Execute(GetPaymentByIdQuery getPaymentByIdQuery, CancellationToken cancellationToken);
}