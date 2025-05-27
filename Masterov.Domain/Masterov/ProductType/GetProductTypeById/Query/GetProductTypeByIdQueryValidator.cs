using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;

public class GetProductTypeByIdQueryValidator : AbstractValidator<GetProductTypeByIdQuery>
{
    public GetProductTypeByIdQueryValidator()
    {
        RuleFor(q => q.ProductTypeId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductTypeId must not be an empty GUID.");
    }
}