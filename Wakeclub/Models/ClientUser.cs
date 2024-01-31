using Wakeclub.Models.Validations;

namespace Wakeclub.Models;

public class ClientUser
{
    [User_EnsureCorrectSingporeNumber]
    public string PhoneNumber { get; set; }
}