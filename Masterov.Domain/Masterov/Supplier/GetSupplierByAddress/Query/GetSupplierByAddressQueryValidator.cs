using FluentValidation;
namespace Masterov.Domain.Masterov.Supplier.GetSupplierByAddress.Query;

public class GetSupplierByAddressQueryValidator : AbstractValidator<GetSupplierByAddressQuery>
{
    public GetSupplierByAddressQueryValidator()
    {
        RuleFor(c => c.Address)
            .NotEmpty()
            .WithMessage("Адрес не может быть пустым")
            .MaximumLength(200)
            .WithMessage("Адрес не может быть длиннее 200 символов");
    }
}