
namespace Wakeclub.Entities;

public class WakeClubPool : BaseEntity
{
    public decimal StakedAmount { get; set; }
    public decimal LostAmount { get; set; }
    public int Players { get; set; }
    public DateTimeOffset Date { get; set; }

    public WakeClubPool()
    {
        this.StakedAmount = 0;
        this.LostAmount = 0;
        this.Players = 0;
        this.Date = DateTimeOffset.UtcNow.Date;
    }
}
