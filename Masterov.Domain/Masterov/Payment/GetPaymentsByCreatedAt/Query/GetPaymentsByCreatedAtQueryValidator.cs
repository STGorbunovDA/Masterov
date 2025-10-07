using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt.Query;

public class GetPaymentsByCreatedAtQueryValidator : AbstractValidator<GetPaymentsByCreatedAtQuery>
{
    public GetPaymentsByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}