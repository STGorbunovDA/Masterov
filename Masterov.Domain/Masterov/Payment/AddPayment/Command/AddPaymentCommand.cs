using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Payment.AddPayment.Command;

public record AddPaymentCommand(Guid OrderId, PaymentMethod PaymentMethod, decimal Amount, string NameCustomer, string? EmailCustomer, string? PhoneCustomer);