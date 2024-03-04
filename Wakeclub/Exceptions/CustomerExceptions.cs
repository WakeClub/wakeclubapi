namespace Wakeclub.Exceptions;

public class CustomerAlreadyRegisteredException : BadRequestException
{
    public const string Message = "Customer is already registered. ";

    public CustomerAlreadyRegisteredException() : base(Message)
    {
    }
    public CustomerAlreadyRegisteredException(string msg)
        : base(Message + msg)
    {
    }
}

public class CustomerDoesNotExistException : NotFoundException
{
    public const string Message = "Customer does not exist. ";

    public CustomerDoesNotExistException() : base(Message)
    {
    }
    public CustomerDoesNotExistException(string msg)
        : base(Message + msg)
    {
    }
}
