namespace Toro.API.Domain.Resources.Result;

public struct GenericCommandResult
{
    public GenericCommandResult(bool success, string message, object data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public GenericCommandResult(bool success, string message)
    {
        Success = success;
        Message = message;
        Data = null;
    }

    public GenericCommandResult(bool success)
    {
        Success = success;
        Data = null;
    }

    public bool Success { get; private set; }
    public string Message { get; private set; }
    public object Data { get; private set; }
}
