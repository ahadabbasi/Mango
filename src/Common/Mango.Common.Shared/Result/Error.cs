namespace Mango.Common.Shared.Result;

public record Error(string Code, string? Description = null)
{
    public static readonly Error None = new Error(string.Empty);

    

    public static implicit operator Error(string code) => new Error(code);


    //public static implicit operator Result<TValue>(Error error) => Result.Failure<TValue>(new[] { error });
}