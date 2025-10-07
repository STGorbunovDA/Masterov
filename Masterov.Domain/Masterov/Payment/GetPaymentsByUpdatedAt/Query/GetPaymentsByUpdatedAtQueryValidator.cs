using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt.Query;

public class GetPaymentsByUpdatedAtQueryValidator : AbstractValidator<GetPaymentsByUpdatedAtQuery>
{
    public GetPaymentsByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}