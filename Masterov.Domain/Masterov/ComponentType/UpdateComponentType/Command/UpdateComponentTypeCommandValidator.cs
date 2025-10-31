using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;

public class UpdateComponentTypeCommandValidator : AbstractValidator<UpdateComponentTypeCommand>
{
    public UpdateComponentTypeCommandValidator()
    {
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("componentTypeId must not be an empty GUID.");
        
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        // Валидация даты создания (CreatedAt)
        RuleFor(c => c.CreatedAt)
            .Must(DomainExtension.BeValidPastOrPresentDate)
            .When(c => c.CreatedAt.HasValue)
            .WithErrorCode("InvalidCreatedAt")
            .WithMessage("Creation date cannot be in the future.");
        
        RuleFor(c => c.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the description should not be more than 200");
        
    }
}