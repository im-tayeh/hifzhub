namespace HifzHub.Domain.Common;

public enum ErrorType
{
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Forbidden = 4
}

public record Error(ErrorType Type, string Message)
{
    public static readonly Error None = new(default, string.Empty);
}

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result NotFound(string msg) => Failure(new Error(ErrorType.NotFound, msg));
    public static Result Validation(string msg) => Failure(new Error(ErrorType.Validation, msg));
    public static Result Conflict(string msg) => Failure(new Error(ErrorType.Conflict, msg));
    public static Result Forbidden(string msg) => Failure(new Error(ErrorType.Forbidden, msg));
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true, Error.None) => Value = value;
    private Result(Error error) : base(false, error) => Value = default;

    public static Result<T> Success(T value) => new(value);
    public static new Result<T> Failure(Error error) => new(error);

    public static new Result<T> NotFound(string msg) => Result<T>.Failure(new Error(ErrorType.NotFound, msg));
    public static new Result<T> Validation(string msg) => Result<T>.Failure(new Error(ErrorType.Validation, msg));
    public static new Result<T> Conflict(string msg) => Result<T>.Failure(new Error(ErrorType.Conflict, msg));
    public static new Result<T> Forbidden(string msg) => Result<T>.Failure(new Error(ErrorType.Forbidden, msg));
}