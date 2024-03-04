using System.ComponentModel.DataAnnotations;
using Wakeclub.Models;

namespace Wakeclub.Entities;

public class Transaction : BaseEntity
{
    public virtual Customer Customer { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public TransactionStatus TransactionStatus { get; private set; }
    public string ReferenceId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentRail PaymentMethod { get; private set; }
    public string Currency { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }

    public Transaction(string referenceId, TransactionType transactionType, TransactionStatus transactionStatus,
        decimal amount, PaymentRail paymentMethod, string currency, string phoneNumber, string email)
    {
        ReferenceId = referenceId;
        TransactionType = transactionType;
        TransactionStatus = transactionStatus;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Currency = currency;
        PhoneNumber = phoneNumber;
        Email = email;
    }
    public void updateCustomer(Customer customer)
    {
        Customer = customer;
    }
    
}