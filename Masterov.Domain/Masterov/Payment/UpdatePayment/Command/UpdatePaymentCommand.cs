using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Payment.UpdatePayment.Command;

public record UpdatePaymentCommand(Guid PaymentId, Guid OrderId, Guid CustomerId, PaymentMethod MethodPayment, decimal Amount, DateTime PaymentDate);