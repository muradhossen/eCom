namespace Application.Common.Result;

public class Result
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get => !IsSuccess; } 
    public string ErrorMessage { get; private set; }
    public Error Error { get;}
    public dynamic Data {  get; private set; }

 
    protected Result(bool isSuccess, Error error, dynamic data = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    } 

    public static Result Success() => new(true, null);
    public static Result Success(dynamic data) => new(true, null, data);
    public static Result Failure(Error error) => new(false, error);
}
