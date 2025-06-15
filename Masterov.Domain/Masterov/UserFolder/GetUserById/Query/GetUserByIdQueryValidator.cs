using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.GetUserById.Query;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
    }
}