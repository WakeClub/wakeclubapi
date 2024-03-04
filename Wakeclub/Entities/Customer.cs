
namespace Wakeclub.Entities;

public class Customer : BaseEntity
{
    public string Id { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? LatestWakeUpId { get; private set; }
    public virtual WakeUp? LatestWakeUp { get; private set; }
    public virtual Wallet? Wallet { get; private set; }
    public string? RecipientId { get; private set; }
    public string BankBranchCode { get; private set; }
    public string BankId { get; private set; }
    public string BankAccountNumber { get; private set; }

    public Customer(string id, string email, string phoneNumber, string bankBranchCode, string bankId, string bankAccountNumber) 
    {
        Id = id;
        Email = email;
        PhoneNumber = phoneNumber;
        BankBranchCode = bankBranchCode;
        BankId = bankId;
        BankAccountNumber = bankAccountNumber;
    }

    public void updatePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber ?? PhoneNumber;
    }

    public void updateWallet(Wallet wallet)
    {
        Wallet = wallet;
    }

    public void updateBankDetails(string bankBranchCode, string bankId, string bankAccountNumber)
    {
        BankBranchCode = bankBranchCode ?? BankBranchCode;
        BankId = bankId ?? BankId;
        BankAccountNumber = bankAccountNumber ?? BankAccountNumber;
    }

    public void addRecipientId(string recipientId)
    {
        RecipientId = recipientId;
    }

    public void updateWakeUp(WakeUp wakeUp)
    {
        LatestWakeUp = wakeUp;
        LatestWakeUpId = wakeUp.Id;
    }
    
}