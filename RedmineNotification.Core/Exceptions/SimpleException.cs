namespace RedmineNotification.Core.Exceptions;

public class SimpleException : Exception
{
    public SimpleException(string message) : base(message)
    {
    }
}
