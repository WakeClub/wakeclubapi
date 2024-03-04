using Org.BouncyCastle.Bcpg;
using Wakeclub.Models;

namespace Wakeclub.Entities;

public class WakeUp : BaseEntity
{ 
    public string CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; }
    public WakeUpStatus Status { get; private set; }
    public decimal Amount { get; private set; }
    public DateTimeOffset WakeUpTime { get; private set; }
    
    public string ImageURL { get; private set; }
    public virtual WakeClubPool WakeClubPool { get; private set; }

    public WakeUp(
        decimal amount,
        DateTimeOffset wakeUpTime
        )
    {
        this.Status = WakeUpStatus.PENDING;
        this.Amount = amount;
        this.WakeUpTime = wakeUpTime;
    }

    public void updateWakeClubPool(WakeClubPool wakeClubPool)
    {
        WakeClubPool = wakeClubPool;
    }

    public void updateCustomer(Customer customer)
    {
        Customer = customer;
        CustomerId = customer.Id;
    }
}