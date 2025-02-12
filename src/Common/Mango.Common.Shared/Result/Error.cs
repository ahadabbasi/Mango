namespace Mango.Common.Shared.Result;

public record Error(string Code, string? Description = null)
{
    public static readonly Error None = new Error(string.Empty);
    
    public static readonly Error NotFound = new Error(string.Empty);
    
    public static readonly Error ServerNotResponse = new Error(string.Empty);
    
    public static readonly Error Unexpected = new Error(string.Empty);
    
    public static implicit operator Error(string code) => new Error(code);
}