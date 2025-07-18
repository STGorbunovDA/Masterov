﻿using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByName.Query;

public class GetCustomerByNameQueryValidator : AbstractValidator<GetCustomerByNameQuery>
{
    public GetCustomerByNameQueryValidator()
    {
        RuleFor(c => c.CustomerName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The сustomerName should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the сustomerName should not be more than 100");
    }
}