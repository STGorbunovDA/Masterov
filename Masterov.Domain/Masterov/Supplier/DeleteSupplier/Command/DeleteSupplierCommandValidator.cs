using FluentValidation;
using Masterov.Domain.Masterov.Customer.DeleteCustomer.Command;

namespace Masterov.Domain.Masterov.Supplier.DeleteSupplier.Command;

public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
{
    public DeleteSupplierCommandValidator()
    {
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");}
}