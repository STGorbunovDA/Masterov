using FluentValidation;
using Masterov.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Masterov.API.Middlewares;

public static class ProblemDetailsFactoryExtensions
{
    public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext httpContext,
        DomainException domainException) =>
        factory.CreateProblemDetails(httpContext,
            domainException.ErrorCode switch
            {
                ErrorCode.StatusCode410 => StatusCodes.Status410Gone,
                ErrorCode.StatusCode409 => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            },
            domainException.Message);
    
    public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext httpContext,
        ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in validationException.Errors)
        {
            modelStateDictionary.AddModelError(error.PropertyName, error.ErrorCode);
        }

        return factory.CreateValidationProblemDetails(httpContext,
            modelStateDictionary,
            StatusCodes.Status400BadRequest,
            "Validation failed");
    }
}