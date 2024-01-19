namespace Wakeclub.Entities;

public class WakeClubPool : BaseEntity
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset Date { get; set; }
}