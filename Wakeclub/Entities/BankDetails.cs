
namespace Wakeclub.Entities;

public class BankDetails : BaseEntity
{
    public Guid Id { get; set; }
    public string BankName { get; set; }
    public string RegisteredName { get; set; }
    public string BankAccountNumber { get; set; }
    public string PhoneNumber { get; set; }
}