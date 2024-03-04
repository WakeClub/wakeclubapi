namespace Wakeclub.Exceptions;

public class PaynowDepositRequestFailedException : InternalServerErrorException
{
    public const string Message = "Hitpay Paynow Request Failed. ";

    public PaynowDepositRequestFailedException() : base(Message)
    {
    }
    public PaynowDepositRequestFailedException(string msg)
        : base(Message + msg)
    {
    }
}

public class PaynowDepositHmacAuthenticationFailedException : InvalidOperationException
{
    public const string Message = "Hitpay Paynow Deposit Validation Failed. ";

    public PaynowDepositHmacAuthenticationFailedException() : base(Message)
    {
    }
    public PaynowDepositHmacAuthenticationFailedException(string msg)
        : base(Message + msg)
    {
    }
}