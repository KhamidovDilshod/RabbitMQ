using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sender.MiddleWares;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occured while processing your request",
            Status = (int) HttpStatusCode.InternalServerError,
            Type = "https://tools.ietf.org/html"
        };
        var exception = context.Exception;
        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;

    }
}