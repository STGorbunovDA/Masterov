using FluentValidation;
using Masterov.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Masterov.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ProblemDetailsFactory problemDetailsFactory)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            var problemDetails = exception switch
            {
                ValidationException validationException =>
                    problemDetailsFactory.CreateFrom(httpContext, validationException),
                DomainException domainException =>
                    problemDetailsFactory.CreateFrom(httpContext, domainException),
                _ => problemDetailsFactory.CreateProblemDetails(httpContext, StatusCodes.Status500InternalServerError,
                    "Unhandled error! Please contact us.", detail: exception.Message),
            };

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            if (problemDetails is ValidationProblemDetails validationProblemDetails)
            {
                await httpContext.Response.WriteAsJsonAsync(validationProblemDetails);
            }
            else
            {
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}