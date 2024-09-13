using System;
using System.Collections.Generic;

namespace CurrencyApiInfrastructure.ExceptionHandling.Dtos;

/// <summary>
/// The error data transfer object.
/// </summary>
[Serializable]
public class ErrorDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorDto"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    public ErrorDto(int status = 400)
    {
        Status = status;
    }

    /// <summary>
    /// Create new instance and set the errors to "CustomError" key in Errors dictionary
    /// </summary>
    public ErrorDto(IEnumerable<string> errors, int status = 400)
    {
        Errors.Add("CustomError", errors);
        Status = status;
    }

    /// <summary>
    /// Create new instance and set only one error text to "CustomError" key in Errors dictionary
    /// </summary>
    public ErrorDto(string errorText, int status = 400)
    {
        Errors.Add("CustomError", new List<string>() { errorText });
        Status = status;
    }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the internal error message.
    /// </summary>
    public string InternalErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the internal stack trace.
    /// </summary>
    public string InternalStackTrace { get; set; }

    /// <summary>
    /// Gets or sets the internal source.
    /// </summary>
    public string InternalSource { get; set; }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    public Dictionary<string, IEnumerable<string>> Errors { get; set; } = new();
}