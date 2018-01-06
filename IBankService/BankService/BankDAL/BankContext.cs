using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BankEntities;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace BankDAL
{
    public class BankContext : DbContext
    {
        public BankContext()
            : base()
        { }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<CardAccount> CardAccounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ArbitraryTransaction> ArbitraryTransactions { get; set; }
        public DbSet<SSISTransaction> SSISTransactions { get; set; }
        public DbSet<MobileTransaction> MobileTransactions { get; set; }
        public DbSet<TransferTransaction> TransferTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
