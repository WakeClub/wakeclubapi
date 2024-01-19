using Microsoft.AspNetCore.Identity;

namespace Wakeclub.Entities;

public class User : IdentityUser
{
    public BankDetails BankDetails { get; set; }
    public Guid? LatestWakeUpId { get; set; }
    public WakeUp? LatestWakeUp { get; set; }
    public Wallet Wallet { get; set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}