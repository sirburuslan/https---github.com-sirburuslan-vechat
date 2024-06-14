/*
 * @class AdministratorAccessAttribute
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-16
 *
 * This class is used to manage the access for administrator to a controller
 */

// System Utils
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// Installed Utils
using MongoDB.Bson;

// App Utils
using api.Models.Dtos;
using api.Services.Interfaces.Users;

// Namespace for attributes
namespace api.Utilities.Attributes;

/// <summary>
/// Administrator Access to a controller
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class AdministratorAccessAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// Filter the request
    /// </summary>
    /// <param name="context">Filters context</param>
    /// <param name="next">A delegate that asynchronously returns an ActionExecutedContext indicating the action or the next action filter has executed.</param>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Verify if authorization exists
        if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            ResponseDto<UserDto>? user = null;

            // Verify if access token exists to get the user's data
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                // Get the access token
                string? token = authHeader.FirstOrDefault()?.Split(" ").LastOrDefault();

                // Verify if access token exists
                if (token is not null)
                {
                    // Get the user's ID
                    string userId = new Tokens().GetTokenData(token ?? string.Empty, "UserId");

                    // Verify if the user id exists
                    if (userId != "")
                    {
                        // Get the repository from the services.
                        var serviceProvider = context.HttpContext.RequestServices;
                        var usersRepository = serviceProvider.GetRequiredService<IUsersRepository>();

                        // Retrieve the user's data
                        ObjectId parsedUserId = ObjectId.Parse(userId);
                        user = await usersRepository.GetUserAsync(parsedUserId);
                    }
                }
            }

            // Verify if user exists
            if (user?.Result != null)
            {
                // Save temporary user in http context item
                context.HttpContext.Items["user"] = user.Result;

                // Go next
                await next();
            }
            else
            {
                // Stop the access
                context.Result = new UnauthorizedResult();
            }
        }
        else
        {
            // Stop the access
            context.Result = new UnauthorizedResult();
        }
    }
}