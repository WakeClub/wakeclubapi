namespace Wakeclub.Exceptions;

public class TransactionDoesNotExistException : NotFoundException
{
    public const string Message = "Transaction Not Found. ";

    public TransactionDoesNotExistException() : base(Message)
    {
    }
    public TransactionDoesNotExistException(string msg)
        : base(Message + msg)
    {
    }
}