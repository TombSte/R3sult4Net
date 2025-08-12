namespace R3sult4Net;

/// <summary>
/// Represents the types of errors that can occur in R3sult4Net.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Indicates a general failure that does not fit into other categories.
    /// </summary>
    Failure,
    /// <summary>
    /// Indicates a not found error, typically when a requested resource does not exist.
    /// </summary>
    NotFound,
    /// <summary>
    /// Indicates a validation error, typically when input data does not meet the required criteria.
    /// </summary>
    Validation,
    /// <summary>
    /// Indicates a conflict error, typically when there is a conflict with the current state of the resource.
    /// </summary>
    Conflict,
    /// <summary>
    /// Indicates an unauthorized error, typically when access is denied due to lack of authentication or authorization.
    /// </summary>
    Unauthorized,
    /// <summary>
    /// Indicates a forbidden error, typically when access is denied even with proper authentication.
    /// </summary>
    Forbidden,
    /// <summary>
    /// Indicates a precondition failed error, typically when a required condition for the operation is not met.
    /// </summary>
    PreconditionFailed
}