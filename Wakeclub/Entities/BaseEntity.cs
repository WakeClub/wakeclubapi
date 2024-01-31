namespace Wakeclub.Entities;

public abstract class BaseEntity
{
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public string Id { get; set; }

    public BaseEntity()
    {
        this.Id = Guid.NewGuid().ToString();
        this.CreatedAt = DateTimeOffset.UtcNow;
    }
}