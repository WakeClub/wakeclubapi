namespace Wakeclub.Exceptions;

public class UserAlreadyRegisteredException : BadRequestException
{
    public const string Message = "User is already registered. ";

    public UserAlreadyRegisteredException() : base(Message)
    {
    }
    public UserAlreadyRegisteredException(string msg)
        : base(Message + msg)
    {
    }
}

public class UserDoesNotExistException : NotFoundException
{
    public const string Message = "User does not exist. ";

    public UserDoesNotExistException() : base(Message)
    {
    }
    public UserDoesNotExistException(string msg)
        : base(Message + msg)
    {
    }
}
