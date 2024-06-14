/*
 * @class Required Validation
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-15
 *
 * This class contains the methods used for email sign in
 */

// System Namespaces
using System.ComponentModel.DataAnnotations;

// Namespace for Validations
namespace api.Utilities.Validations;

/// <summary>
/// RequiredValidationAttribute class
/// </summary>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class RequiredValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Error Message Resource Name
    /// </summary>
    public new string ErrorMessageResourceName { get; set; } = string.Empty;

    /// <summary>
    /// Type Of Error Messages class
    /// </summary>
    public new Type? ErrorMessageResourceType { get; set; }

    /// <summary>
    /// Validate Request
    /// </summary>
    /// <param name="value">Object with value</param>
    /// <param name="validationContext">Valdation Context</param>
    /// <returns></returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            // Error message container
            string errorMessage;

            if (!string.IsNullOrEmpty(ErrorMessageResourceName) && ErrorMessageResourceType != null)
            {
                var property = ErrorMessageResourceType.GetProperty(ErrorMessageResourceName);
                if (property != null)
                {
                    errorMessage = (string)property.GetValue(null)!;
                }
                else
                {
                    errorMessage = ErrorMessage ?? string.Empty; // Default error message
                }
            }
            else
            {
                errorMessage = ErrorMessage ?? string.Empty; // Default error message
            }

            return new ValidationResult(errorMessage);
        }

#pragma warning disable CS8603 // Possible null reference return.
        return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
    }
}