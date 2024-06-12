namespace AdvertisingAgency.Application.Exceptions;

public sealed class ObjectNotFoundException(string objectName) : Exception
{
    public override string Message { get; } = $"Object of type {objectName} not found";
}
