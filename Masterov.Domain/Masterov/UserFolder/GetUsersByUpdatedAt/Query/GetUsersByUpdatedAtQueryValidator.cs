using FluentValidation;
using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt.Query;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt.Query;

public class GetUsersByUpdatedAtQueryValidator : AbstractValidator<GetUsersByUpdatedAtQuery>
{
    public GetUsersByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}