using FluentValidation;

namespace Masterov.Domain.Masterov.Product.GetProductById.Query;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(q => q.ProductId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductId must not be an empty GUID.");
    }
}