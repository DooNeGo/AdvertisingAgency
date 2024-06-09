namespace AdvertisingAgency.Domain.Exceptions;

public sealed class NotLoggedInException : Exception
{
    public override string Message => "User not logged in";
}