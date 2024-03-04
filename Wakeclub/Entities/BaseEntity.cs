namespace Wakeclub.Entities;

public abstract class BaseEntity
{
    public DateTimeOffset CreatedAt { get; private set; }
    public string Id { get; private set; }

    public BaseEntity()
    {
        this.Id = Guid.NewGuid().ToString();
        this.CreatedAt = DateTimeOffset.UtcNow;
    }

    public void overrideId(string id)
    {
        Id = id;
    }
}