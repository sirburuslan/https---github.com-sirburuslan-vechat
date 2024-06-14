/*
 * @class UpdateController
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-21
 *
 * This class is used to update the administrator settings
 */

// System Utils
using System.Reflection;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// App Utils
using api.Models.Dtos;
using api.Models.Entities.Users;
using api.Services.Interfaces.Users;
using api.Utilities;
using api.Utilities.Attributes;

// Namespace for Users Administrator Controller
namespace api.Controllers.Administrator.Users.Options;

/// <summary>
/// Initializes a new instance of the <see cref="UpdateController"/> class.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/admin/users")]
[AdministratorAccess]
public class UpdateController(IUsersRepository usersRepository) : ControllerBase
{
    /// <summary>
    /// Gets the user information
    /// </summary>
    /// <returns>Success message or error message for save changes</returns>
    [HttpPost("options")]
    [EnableCors("MainPolicy")]
    public async Task<IActionResult> Options([FromBody] UserOptionsDto userOptionsDto)
    {

        // Get the user data
        UserDto? user = HttpContext.Items["user"] as UserDto;

        // Get all users options
        Check.IsNotNull(user, nameof(user), Words.Get("AccountNotFound"));
        ResponseDto<List<UserOptionDto>> optionsList = await usersRepository.OptionsListAsync(user!.UserId);

        // Options to update container
        List<UserOptionDto> optionsUpdate = [];

        // Saved options names
        List<string> optionsSaved = [];

        // Check if options exists
        if (optionsList.Result != null)
        {

            // Get options length
            int optionsLength = optionsList.Result.Count;

            // List the saved options
            for (int o = 0; o < optionsLength; o++)
            {

                // Get option's name
                var optionName = typeof(UserOptionsDto).GetProperty(optionsList.Result[o].OptionName);

                // Verify if option's name is not null
                if (optionName != null)
                {

                    // Verify if option name is plan id
                    if (optionName.Name == "PlanId")
                    {
                        continue;
                    }

                    // Get the option's value
                    var optionValue = optionName!.GetValue(userOptionsDto);

                    // Verify if optionValue is not null
                    if (optionValue == null)
                    {
                        continue;
                    }

                    // Save the option's name
                    optionsSaved.Add(optionsList.Result[o].OptionName);

                    // Create the option's params
                    UserOptionDto optionUpdate = new()
                    {
                        OptionId = optionsList.Result[o].OptionId,
                        UserId = optionsList.Result[o].UserId,
                        OptionName = optionsList.Result[o].OptionName,
                        OptionValue = optionValue!.ToString() ?? string.Empty
                    };

                    // Add option in the update list
                    optionsUpdate.Add(optionUpdate);

                }

            }

        }

        // Options to save container
        List<UserOptionDto> optionsSave = [];

        // Get all options dto properties
        PropertyInfo[] propertyInfos = typeof(UserOptionsDto).GetProperties();

        // Get properties length
        int propertiesLength = propertyInfos.Length;

        // List the properties
        for (int p = 0; p < propertiesLength; p++)
        {

            // If is UserId continue
            if (propertyInfos[p].Name == "UserId")
            {
                continue;
            }

            // Verify if option name is plan id
            if (propertyInfos[p].Name == "PlanId")
            {
                continue;
            }

            // If value is null continue
            if (propertyInfos[p].GetValue(userOptionsDto) == null)
            {
                continue;
            }

            // Check if the option is not saved already
            if (!optionsSaved.Contains(propertyInfos[p].Name))
            {

                // Create the option
                UserOptionDto option = new()
                {
                    UserId = user!.UserId,
                    OptionName = propertyInfos[p].Name,
                    OptionValue = propertyInfos[p].GetValue(userOptionsDto)!.ToString() ?? string.Empty
                };

                // Add option to the save list
                optionsSave!.Add(option);

            }

        }

        // Errors counter
        int errors = 0;

        // Verify if options for updating exists
        if (optionsUpdate.Count > 0)
        {

            // Update options
            bool update_options = await usersRepository.UpdateOptionsAsync(optionsUpdate);

            // Check if an error has been occurred when the options were updated
            if (!update_options)
            {
                errors++;
            }

        }

        // Verify if options for saving exists
        if (optionsSave.Count > 0)
        {

            // Save options
            bool save_options = await usersRepository.SaveOptionsAsync(optionsSave);

            // Check if an error has been occurred when the options were saved
            if (!save_options)
            {
                errors++;
            }

        }

        // Verify if no errors occurred
        if (errors == 0)
        {

            // Return a json
            return new JsonResult(new
            {
                success = true,
                message = Words.Get("UsersSettingsUpdated")
            });

        }
        else
        {

            // Return a json
            return new JsonResult(new
            {
                success = false,
                message = Words.Get("UsersSettingsNotUpdated")
            });

        }

    }
}