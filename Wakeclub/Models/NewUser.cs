using System.ComponentModel.DataAnnotations;
using Wakeclub.Models.Validations;

namespace Wakeclub.Models;

public class NewUser
{
    [User_EnsureCorrectSingporeNumber]
    public string PhoneNumber { get; set; }
}