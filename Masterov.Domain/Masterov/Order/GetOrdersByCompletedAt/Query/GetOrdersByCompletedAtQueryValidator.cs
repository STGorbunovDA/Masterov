using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt.Query;

public class GetOrdersByCompletedAtQueryValidator : AbstractValidator<GetOrdersByCompletedAtQuery>
{
    public GetOrdersByCompletedAtQueryValidator()
    {
        RuleFor(q => q.CompletedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode("InvalidDate")
            .WithMessage("CompletedAt date cannot be in the future.");
    }
}