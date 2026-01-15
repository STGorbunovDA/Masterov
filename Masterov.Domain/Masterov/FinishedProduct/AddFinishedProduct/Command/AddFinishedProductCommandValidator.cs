using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;

public class AddFinishedProductCommandValidator : AbstractValidator<AddFinishedProductCommand>
{
    public AddFinishedProductCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(c => c.Type).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The type should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the type should not be more than 100");
        
        RuleFor(c => c.Price).Cascade(CascadeMode.Stop)
            .Cascade(CascadeMode.Stop)
            .Must(price => price == null || price > 0)
            .WithErrorCode("Invalid")
            .WithMessage("The price should be greater than 0 if specified.")
            .Must(price => price == null || DomainExtension.HasValidPrecisionAndScale(price.Value, 18, 2))
            .WithErrorCode("InvalidFormat")
            .WithMessage("The price should have maximum 2 decimal places and no more than 18 digits total.");


        
        When(c => c.Width.HasValue, () =>
        {
            RuleFor(c => c.Width)
                .GreaterThan(0)
                .WithErrorCode("Invalid")
                .WithMessage("The width should be greater than 0.");
        });
        
        When(c => c.Height.HasValue, () =>
        {
            RuleFor(c => c.Height)
                .GreaterThan(0)
                .WithErrorCode("Invalid")
                .WithMessage("The height should be greater than 0.");
        });
        
        When(c => c.Depth.HasValue, () =>
        {
            RuleFor(c => c.Depth)
                .GreaterThan(0)
                .WithErrorCode("Invalid")
                .WithMessage("The depth should be greaater than 0.");
        });
        
        RuleFor(x => x.Image).Cascade(CascadeMode.Stop)
            .Cascade(CascadeMode.Stop)
            .Must(content => content == null || (content.Length > 0 && content.Length <= 100 * 1024 * 1024))
            .WithMessage("If a file is provided, it must not be empty and must be no more than 100 MB.")
            .Must(content => content == null || DomainExtension.IsImage(content))
            .WithMessage("The file must be an image.");
        
        RuleFor(c => c.Elite)
            .NotNull()
            .WithErrorCode("EliteRequired")
            .WithMessage("Elite must be specified.");
        
        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(300)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 300 characters.");
    }
}