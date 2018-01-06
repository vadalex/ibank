using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankEntities;
using System.Data.Entity;

namespace BankDAL
{
    public class Repositories : IDisposable
    {
        private BankContext context;

        public IRepository<BankAccount> BankAccounts { get; set; }
        public IRepository<CardAccount> CardAccounts { get; set; }
        public IRepository<Currency> Currency { get; set; }
        public IRepository<Employee> Employees { get; set; }
        public IRepository<ArbitraryTransaction> ArbitraryTransactions { get; set; }
        public IRepository<MobileTransaction> MobileTransactions { get; set; }
        public IRepository<SSISTransaction> SSISTransactions { get; set; }
        public IRepository<TransferTransaction> TransferTransactions { get; set; }

        public Repositories()
        {
            Database.SetInitializer(new BankInitializer());

            this.context = new BankContext();            
            CardAccounts = new DataRepository<CardAccount>(context);
            BankAccounts = new DataRepository<BankAccount>(context);
            Employees = new DataRepository<Employee>(context);
            Currency = new DataRepository<Currency>(context);
            ArbitraryTransactions = new DataRepository<ArbitraryTransaction>(context);
            MobileTransactions = new DataRepository<MobileTransaction>(context);
            SSISTransactions = new DataRepository<SSISTransaction>(context);
            TransferTransactions = new DataRepository<TransferTransaction>(context);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (context != null)
            {
                if (disposing)
                {
                    context.Dispose();
                    context = null;
                }
            }            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
