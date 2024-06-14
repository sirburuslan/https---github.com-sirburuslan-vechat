/*
 * @class User Options Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-20
 *
 * This class is used for user options data transfer
 */

// Installed Utils
using MongoDB.Bson;

// App Utils
using api.Utilities.Validations;

// Namespace for Dtos
namespace api.Models.Dtos;

/// <summary>
/// Dto for User Options
/// </summary>
public class UserOptionsDto
{
    /// <summary>
    /// User's ID
    /// </summary>
    public ObjectId UserId { get; set; }

    /// <summary>
    /// Sidebar status
    /// </summary>
    [NumberValidation(Minimum = 0, Maximum = 1, ErrorMessage = "SupportedValueShouldBe")]
    public int? SidebarStatus { get; set; }

    /// <summary>
    /// Users chart time
    /// </summary>
    [NumberValidation(Minimum = 1, Maximum = 4, ErrorMessage = "SupportedValueShouldBe")]
    public int? UsersChartTime { get; set; }

    /// <summary>
    /// Threads chart time
    /// </summary>
    [NumberValidation(Minimum = 1, Maximum = 4, ErrorMessage = "SupportedValueShouldBe")]
    public int? ThreadsChartTime { get; set; }
}