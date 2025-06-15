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
            .MaximumLength(50)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 50");
        
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
            .WithMessage("Если файл предоставлен, он не должен быть пустым и должен быть не больше 100 МБ.")
            .Must(content => content == null || DomainExtension.IsImage(content))
            .WithMessage("Файл должен быть изображением.");
    }
}