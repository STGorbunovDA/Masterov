using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt.Query;

public class GetUsersByCreatedAtQueryValidator : AbstractValidator<GetUsersByCreatedAtQuery>
{
    public GetUsersByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}