/*
 * @class Checker
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-20
 *
 * This class is check if the content is valid
 */

// Namespace for General Utils
namespace api.Utilities;

/// <summary>
/// Checks if the content is valid
/// </summary>
public static class Check
{

    /// <summary>
    /// Ensure the value is not null
    /// </summary>
    /// <typeparam name="T">Represents the type of the value</typeparam>
    /// <param name="value">Contains the value</param>
    /// <param name="name">Contains the name of the parameter</param>
    /// <param name="message">Custom message</param>
    /// <returns>The value of parameter</returns>
    public static T IsNotNull<T>(T value, string name, string message) where T : class?
    {

        if (value == null)
        {
            throw new ArgumentNullException(name, message);
        }

        return value;

    }

}