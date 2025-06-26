namespace Mango.Common.Shared.Result;

public record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);

    public static readonly Error NotFound = new(string.Empty);

    public static readonly Error ServerNotResponse = new(string.Empty);

    public static readonly Error Unexpected = 
        new(
            "UnknownException", 
            "An unexpected error has occurred; please try again later."
        );

    public static implicit operator Error(string code) => new(code);
}