using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace DAL
{
    public class IBankContext : DbContext
    {
        public IBankContext()
            : base("IBankConnectionString")
        { }
       
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccessCard> AccessCards { get; set; }
        public DbSet<AccessCode> AccessCodes { get; set; }        
        public DbSet<SSISPayment> SSISPayments { get; set; }
        public DbSet<OwnSSISPayment> OwnSSISPayments { get; set; }
        public DbSet<OwnArbitraryPayment> OwnArbitraryPayments { get; set; }
        public DbSet<OwnMobilePayment> OwnMobilePayments { get; set; }
        public DbSet<OwnTransferPayment> OwnTransferPayments { get; set; }
        public DbSet<SSISGroup> SSISGroups { get; set; }
        public DbSet<MobileAutoPay> MobileAutoPayments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OwnSSISPayment>().HasRequired<SSISPayment>(e => e.SSISPayment).WithMany(e => e.OwnSSISPayments).WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();            
        }           
    }   
}

