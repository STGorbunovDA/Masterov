﻿using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;

public class GetProductionOrderByIdQueryValidator : AbstractValidator<GetProductionOrderByIdQuery>
{
    public GetProductionOrderByIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}