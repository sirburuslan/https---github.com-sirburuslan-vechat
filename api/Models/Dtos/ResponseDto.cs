﻿/*
 * @class Response Dto
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-19
 *
 * This class is used to handle the responses
 */

// Namespace for Dtos
namespace api.Models.Dtos;

/// <summary>
/// A generic type for responses
/// </summary>
public class ResponseDto<T>
{
    /// <summary>
    /// Set response
    /// </summary>
    public required T? Result { get; set; }

    /// <summary>
    /// Message for the response
    /// </summary>
    public required string? Message { get; set; }
}