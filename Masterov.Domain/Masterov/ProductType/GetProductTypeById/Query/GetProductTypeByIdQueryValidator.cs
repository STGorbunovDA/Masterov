using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;

public class GetProductTypeByIdQueryValidator : AbstractValidator<GetProductTypeByIdQuery>
{
    public GetProductTypeByIdQueryValidator()
    {
        RuleFor(q => q.ProductTypeById).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductTypeById must not be an empty GUID.");
    }
}