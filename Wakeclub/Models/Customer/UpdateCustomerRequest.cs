using Wakeclub.Models.Validations;

namespace Wakeclub.Models;

public class UpdateCustomerRequest
{
    [User_EnsureCorrectSingporeNumber]
    public string PhoneNumber { get; set; }
    public string BankBranchCode { get; private set; }
    public string BankId { get; private set; }
    public string BankAccountNumber { get; private set; }
}