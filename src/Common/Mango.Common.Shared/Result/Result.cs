using System.Collections.Generic;

namespace Mango.Common.Shared.Result;

public class Result
{
    protected Result()
    {
        IsSuccess = true;
    }
    
    protected Result(Error[] errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    
    public bool IsSuccess { get; }

    public IEnumerable<Error>? Errors { get; }

    public static Result Success() => new();
    
    public static Result Failure(Error[] errors) => new(errors);
    
    public static implicit operator Result(Error error) => new[] { error };
    
    public static implicit operator Result(Error[] errors) => Failure(errors);

    public static implicit operator bool(Result result) => result.IsSuccess;

}

