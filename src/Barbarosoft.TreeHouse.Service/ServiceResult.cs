namespace Barbarosoft.TreeHouse.Service;

public class ServiceResult
{
    public string? Message { get; set; }
    public bool IsValid { get; }
    public bool IsSuccess => IsValid;
    public ServiceResult() => IsValid = true;
    public ServiceResult(string message)
    {
        IsValid = false;
        Message = message;
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }
    public ServiceResult(T data) : base()
    {
        Data = data;
    }
    public ServiceResult(string message) : base(message)
    {
        Data = default;
    }
    // Implicit operator for data
    public static implicit operator ServiceResult<T>(T data) => new ServiceResult<T>(data);

    // Implicit operator for exception
    public static implicit operator ServiceResult<T>(string message) => new ServiceResult<T>(message);
}
