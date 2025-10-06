using FluentValidation;
using Masterov.Domain.Masterov.UserFolder.ChangeUpdatedAtUserById.Command;

namespace Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById.Command;

public class ChangeAccountLoginDateUserByIdCommandValidator : AbstractValidator<ChangeAccountLoginDateUserByIdCommand>
{
    public ChangeAccountLoginDateUserByIdCommandValidator()
    {
        RuleFor(q => q.UserId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
        
        RuleFor(q => q.AccountLoginDate).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("AccountLoginDate date cannot be in the future.");
    }
}