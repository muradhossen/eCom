namespace Application.Common.Result;

public class Result
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get => !IsSuccess; } 
    public string ErrorMessage { get; private set; }
    public Error Error { get;}

 
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null); 
    public static Result Failure(Error error) => new(false, error);
}
