using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.DeleteProductType.Command;

public class DeleteDeceasedCommandValidator : AbstractValidator<DeleteProductTypeCommand>
{
    public DeleteDeceasedCommandValidator()
    {
        RuleFor(q => q.ProductTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductTypeId must not be an empty GUID.");
    }
}