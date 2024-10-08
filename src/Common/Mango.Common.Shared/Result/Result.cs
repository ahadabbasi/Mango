using System.Collections.Generic;

namespace Mango.Common.Shared.Result;

public class Result
{
    protected internal Result()
    {
        IsSuccess = true;
    }
    
    protected internal Result(Error[] errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    
    public bool IsSuccess { get; }

    public IEnumerable<Error>? Errors { get; }

    public static Result Success() => new Result();
    
    public static Result Failure(Error[] errors) => new Result(errors);
    
    public static implicit operator Result(Error error) => Result.Failure(new[] { error });
    
}

