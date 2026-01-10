namespace TeamManager.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string email)
        : base($"A user with email '{email}' already exists.")
    {
    }

    public DuplicateEmailException(string email, Exception innerException)
        : base($"A user with email '{email}' already exists.", innerException)
    {
    }
}