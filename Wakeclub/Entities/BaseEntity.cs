namespace Wakeclub.Entities;

public class BaseEntity
{
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}