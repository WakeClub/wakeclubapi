using Microsoft.EntityFrameworkCore;

namespace Wakeclub.Entities;

public class Wallet : BaseEntity
{
    public Guid Id { get; set; }
    public required decimal Amount { get; set; }
    public int Withdrawable { get; set; } = 0;
}