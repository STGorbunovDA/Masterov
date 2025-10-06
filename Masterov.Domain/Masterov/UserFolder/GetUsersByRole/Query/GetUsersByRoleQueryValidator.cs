using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByRole.Query;

public class GetUsersByRoleQueryValidator : AbstractValidator<GetUsersByRoleQuery>
{
    public GetUsersByRoleQueryValidator()
    {
        RuleFor(q => q.role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidRole")
            .WithMessage("Role must be a valid value")
            .Must(role => role != UserRole.Unknown)
            .WithErrorCode("InvalidRoleValue")
            .WithMessage("Role cannot be 'Unknown'");
    }
}