using Microsoft.AspNetCore.Identity;

namespace Identity.Entity;

public class User : IdentityUser
{
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}