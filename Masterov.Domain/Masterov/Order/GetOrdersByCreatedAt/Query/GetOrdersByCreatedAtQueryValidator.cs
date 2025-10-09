using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt.Query;

public class GetOrdersByCreatedAtQueryValidator : AbstractValidator<GetOrdersByCreatedAtQuery>
{
    public GetOrdersByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}