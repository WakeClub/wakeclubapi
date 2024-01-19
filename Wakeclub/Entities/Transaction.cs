using System.ComponentModel.DataAnnotations;
using Wakeclub.Models;

namespace Wakeclub.Entities;

public class Transaction : BaseEntity
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionStatus TrsnsactionStatus { get; set; }
    public decimal Amount { get; set; }
}