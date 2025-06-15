using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;

public class GetFinishedProductByIdQueryValidator : AbstractValidator<GetFinishedProductByIdQuery>
{
    public GetFinishedProductByIdQueryValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductId must not be an empty GUID.");
    }
}