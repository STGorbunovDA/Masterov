﻿using FluentValidation;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;

namespace Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;

public class UpdateFinishedProductCommandValidator : AbstractValidator<UpdateFinishedProductCommand>
{
    public UpdateFinishedProductCommandValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("FinishedProductId must not be an empty GUID.");
        
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
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
    }
}