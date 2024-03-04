using System.ComponentModel.DataAnnotations;
using Wakeclub.Models.Validations;

namespace Wakeclub.Models;

public class NewCustomerRequest
{
    [User_EnsureCorrectSingporeNumber]
    public string PhoneNumber { get; set; }
    public string BankBranchCode { get; set; }
    public string BankId { get; set; }
    public string BankAccountNumber { get; set; }
}