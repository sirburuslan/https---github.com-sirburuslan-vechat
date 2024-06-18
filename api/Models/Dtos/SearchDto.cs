/*
 * @class Elements Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-16
 *
 * This class is used to transfer the response with generic elements
 */

// Namespace for Dtos
namespace api.Models.Dtos;

/// <summary>
/// Search Dto
/// </summary>
public class SearchDto {

    /// <summary>
    /// Search field
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Page number field
    /// </summary>
    public int Page { get; set; }

}