public class Response<T> 
{
    public Response(T data, bool isSuccess, string message)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
    }

    public T Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
}

public class Response 
{
    public Response(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
}