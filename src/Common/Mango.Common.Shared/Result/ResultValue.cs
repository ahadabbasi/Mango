using System;

namespace Mango.Common.Shared.Result;

public sealed class Result<TValue> : Result
{
    private readonly TValue? _value;
    
    protected internal Result(TValue value) : base()
    {
        _value = value;
    }
    
    protected internal Result(Error[] errors) : base(errors)
    {
    }

    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException(string.Empty);
    
    public static Result<T> Success<T>(T value) => new(value);

    public static Result<T> Failure<T>(Error[] errors) => new(errors);
    
    public static implicit operator Result<TValue>(TValue? value) => value != null ? Success<TValue>(value) : Error.NotFound;
    
    public static implicit operator Result<TValue>(Error error) => new[] { error };
    
    public static implicit operator Result<TValue>(Error[] errors) => Failure<TValue>(errors);
}