using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription.Query;

public class GetComponentTypesByDescriptionValidator : AbstractValidator<GetComponentTypesByDescriptionQuery>
{
    public GetComponentTypesByDescriptionValidator()
    {
        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 200 characters.");
    }
}