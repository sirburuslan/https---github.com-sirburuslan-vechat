/*
 * @class New User Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-13
 *
 * This class is used to create new users from the administrator panel
 */

// System Namespaces
using System.ComponentModel.DataAnnotations;

// App Namespaces
using api.Utilities;
using api.Utilities.Validations;

// Namespace for Users Administrator Dto
namespace api.Models.Dtos.Administrator.Users;

/// <summary>
/// New User Dto Class
/// </summary>
public class NewUserDto
{
    /// <summary>
    /// User First Name Field
    /// </summary>
    [StringLength(100, MinimumLength = 1, ErrorMessageResourceName = "FirstNameWrongLength", ErrorMessageResourceType = typeof(ErrorMessages))]
    public string? FirstName { get; set; }
    /// <summary>
    /// User Last Name Field
    /// </summary>
    [StringLength(100, MinimumLength = 1, ErrorMessageResourceName = "LastNameWrongLength", ErrorMessageResourceType = typeof(ErrorMessages))]
    public string? LastName { get; set; }    
    /// <summary>
    /// User Email Field
    /// </summary>
    [RequiredValidation(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(200, ErrorMessageResourceName = "EmailLong", ErrorMessageResourceType = typeof(ErrorMessages))]
    public string? Email { get; set; }

    /// <summary>
    /// User Password Field
    /// </summary>
    [RequiredValidation(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(20, ErrorMessageResourceName = "PasswordLong", ErrorMessageResourceType = typeof(ErrorMessages))]
    public string? Password { get; set; }

    /// <summary>
    /// User's role field
    /// </summary>
    [NumberValidation(Minimum = 0, Maximum = 1, ErrorMessage = "SupportedValueShouldBe")]
    public int Role { get; set; }
}