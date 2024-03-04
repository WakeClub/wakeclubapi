using Wakeclub.Models;

namespace Wakeclub.Services;

public interface IPaynowDepositService
{
    public Task<CreatePaynowDepositResponse> CreatePaynowDeposit(string amount, string email);

    public bool verify(ReceivePaynowDepositWebhookRequest request);
}