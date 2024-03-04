using System.Data.Common;
using Identity.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wakeclub.Entities;

namespace Wakeclub.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<WakeUp> WakeUps { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<WakeClubPool> WakeClubPools { get; set; }
    
    public DbSet<Wallet> Wallets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>()
            .HasOne(e => e.LatestWakeUp)
            .WithOne(e => e.Customer)
            .HasForeignKey<WakeUp>(e => e.CustomerId);
        
        modelBuilder.Entity<WakeUp>()
            .HasOne(e => e.Customer)
            .WithOne(e => e.LatestWakeUp)
            .HasForeignKey<Customer>(e => e.LatestWakeUpId);
    }
}