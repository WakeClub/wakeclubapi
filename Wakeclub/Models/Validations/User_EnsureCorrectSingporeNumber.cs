using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Wakeclub.Models.Validations;

public class User_EnsureCorrectSingporeNumber : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var phoneNumber = value as string;
        Console.WriteLine(phoneNumber + "number");
        var singaporePhoneNumberPattern = @"^[89]\d{7}$";
        Console.WriteLine(Regex.IsMatch(phoneNumber, singaporePhoneNumberPattern));
        if (phoneNumber == null || !Regex.IsMatch(phoneNumber, singaporePhoneNumberPattern))
        {
            return new ValidationResult("Invalid Singapore Phone Number");
        }
        return ValidationResult.Success;
    }
}