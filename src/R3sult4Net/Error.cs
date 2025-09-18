namespace R3sult4Net;

public class Error
{
    public string Description { get; private set;  }
    public string Code { get; private set; }
    public string? Domain { get; set; }
    public Error[]? InnerErrors { get; private set; }

    public Error(string code, string description, string? domain = null, Error[]? errors = null)
    {
        Code = code;
        Description = description;
        Domain = domain;
        InnerErrors = errors ?? [];
    }
}