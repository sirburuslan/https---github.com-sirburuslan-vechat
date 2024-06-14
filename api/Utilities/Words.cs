/*
 * @class Words
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-17
 *
 * This class is used to read the strings resources
 */

// System Namespaces
using System.Resources;

// General Utils namespace
namespace api.Utilities;

/// <summary>
/// This class was created to manage better the strings
/// </summary>
public static class Words
{
    /// <summary>
    /// Get a string by name and culture
    /// </summary>
    /// <param name="name">Name of the string</param>
    /// <returns>string with value</returns>
    public static string Get(string name)
    {
        // Init the Resource Manager class
        ResourceManager rm = new("api.Resources.Strings", typeof(Words).Assembly);

        string? words = rm.GetString(name);

        // Return string or empty
        return words ?? "";
    }
}