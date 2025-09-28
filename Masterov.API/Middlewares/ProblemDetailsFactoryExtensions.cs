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
                ErrorCode.StatusCode401 => StatusCodes.Status401Unauthorized,
                ErrorCode.StatusCode404 => StatusCodes.Status404NotFound,
                ErrorCode.StatusCode410 => StatusCodes.Status410Gone,
                ErrorCode.StatusCode409 => StatusCodes.Status409Conflict,
                ErrorCode.StatusCode422 => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            },
            detail: domainException.Message);
    
    public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext httpContext,
        ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in validationException.Errors)
        {
            var combinedMessage = $"{error.ErrorCode}: {error.ErrorMessage}";
            modelStateDictionary.AddModelError(error.PropertyName, combinedMessage);
        }

        return factory.CreateValidationProblemDetails(httpContext,
            modelStateDictionary,
            StatusCodes.Status400BadRequest,
            "Validation failed");
    }
}