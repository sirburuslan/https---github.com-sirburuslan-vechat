/*
 * @class Exceptions Filter
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-21
 *
 * This scope of this class is to turn in json the catched exceptions
 */

// System Utils
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// Namespace for Extensions
namespace api.Services.Extensions;

/// <summary>
/// Json Exception Filter
/// </summary>
public class JsonExceptionFilter : IExceptionFilter
{

    /// <summary>
    /// Turn exception message in json
    /// </summary>
    /// <param name="context">A context for exception filters</param>
    public void OnException(ExceptionContext context)
    {

        // Re-create the error response
        var errorResponse = new
        {
            success = false,
            message = context.Exception.Message
        };

        // Set the status code to 200 Bad Request
        context.HttpContext.Response.StatusCode = 200;

        // Replace the original response with the JSON error response
        context.Result = new JsonResult(errorResponse);
        context.ExceptionHandled = true;

    }

}