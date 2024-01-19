using Wakeclub.Models;

namespace Wakeclub.Entities;

public class WakeUp : BaseEntity
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required User User { get; set; }
    public required WakeUpStatus Status { get; set; } =  WakeUpStatus.PENDING;
    public required decimal Amount { get; set; }
    public required DateTimeOffset Time { get; set; }
    public string ImageURL { get; set; }
}