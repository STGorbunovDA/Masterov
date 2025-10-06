using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate.Query;

public class GetUsersByAccountLoginDateQueryValidator : AbstractValidator<GetUsersByAccountLoginDateQuery>
{
    public GetUsersByAccountLoginDateQueryValidator()
    {
        RuleFor(q => q.AccountLoginDate).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("AccountLoginDate date cannot be in the future.");
    }
}