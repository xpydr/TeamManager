namespace TeamManager.Data.Exceptions;

public class SchedulingConflictException : Exception
{
    public SchedulingConflictException(string message)
        : base(message)
    {
    }

    public SchedulingConflictException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}