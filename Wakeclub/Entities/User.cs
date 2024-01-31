using Microsoft.AspNetCore.Identity;

namespace Wakeclub.Entities;

public class User : IdentityUser
{
    public virtual BankDetails? BankDetails { get; set; }
    public string? LatestWakeUpId { get; set; }
    public virtual WakeUp? LatestWakeUp { get; set; }
    public virtual Wallet? Wallet { get; set; }
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}