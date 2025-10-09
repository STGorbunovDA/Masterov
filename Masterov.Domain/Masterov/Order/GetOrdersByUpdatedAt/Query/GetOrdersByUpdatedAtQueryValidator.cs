﻿using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt.Query;

public class GetOrdersByUpdatedAtQueryValidator : AbstractValidator<GetOrdersByUpdatedAtQuery>
{
    public GetOrdersByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}