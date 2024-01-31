
namespace Wakeclub.Entities;

public class BankDetails : BaseEntity
{
    public string BankName { get; set; }
    public string RegisteredName { get; set; }
    public string BankAccountNumber { get; set; }
    public string PhoneNumber { get; set; }

    public BankDetails(string phoneNumber) : base()
    {
        this.BankName = String.Empty;
        this.RegisteredName = String.Empty;
        this.BankAccountNumber = String.Empty;
        this.PhoneNumber = phoneNumber;
    }
}