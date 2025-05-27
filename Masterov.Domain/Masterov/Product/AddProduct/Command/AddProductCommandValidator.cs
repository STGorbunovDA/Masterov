using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Product.AddProduct.Command;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(50)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 50");
        
        RuleFor(c => c.Type)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The type should not be empty.")
            .MaximumLength(50)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the type should not be more than 50");
        
        RuleFor(c => c.Price)
            .NotNull()
            .WithErrorCode("Null")
            .WithMessage("The price should not be null.")
            .GreaterThan(0)
            .WithErrorCode("Invalid")
            .WithMessage("The price should be greater than 0.")
            .Must(price => DomainExtension.HasValidPrecisionAndScale(price!.Value, 18, 2))
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
                .WithMessage("The depth should be greater than 0.");
        });
        
        RuleFor(x => x.Content)
            .Must(content => content == null || (content.Length > 0 && content.Length <= 100 * 1024 * 1024))
            .WithMessage("Если файл предоставлен, он не должен быть пустым и должен быть не больше 100 МБ.")
            .When(x => x.Content != null)
            .Must(content => content == null || DomainExtension.IsImage(content))
            .WithMessage("Файл должен быть изображением.")
            .When(x => x.Content != null);
        
    }
}