using Microsoft.EntityFrameworkCore;

namespace Wakeclub.Entities;

public class Wallet : BaseEntity
{
    public decimal Amount { get; set; }
    public int Withdrawable { get; set; } = 0;

    public Wallet() : base()
    {
        this.Amount = GlobalConstants.freePoints;
        this.Withdrawable = 0;
    }
}