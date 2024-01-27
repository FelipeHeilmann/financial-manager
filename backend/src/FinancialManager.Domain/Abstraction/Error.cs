namespace FinancialManager.Domain.Abstraction;
public record Error
{
    public string Code { get;}
    public string Description { get;}
    public ErrorType Type { get;}

    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
    public static Error Validation(string code, string description) => new (code, description, ErrorType.Validation);
    public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);
}

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2, Conflict = 3,
}

