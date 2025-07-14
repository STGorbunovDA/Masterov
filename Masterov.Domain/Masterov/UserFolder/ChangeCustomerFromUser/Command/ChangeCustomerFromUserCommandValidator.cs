using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser.Command;

public class ChangeCustomerFromUserCommandValidator : AbstractValidator<ChangeCustomerFromUserCommand>
{
    public ChangeCustomerFromUserCommandValidator()
    {
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
        
        RuleFor(q => q.UserId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
    }
}