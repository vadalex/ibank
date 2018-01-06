using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.Entity;
namespace DAL
{
    public class Repositories : IDisposable
    {
        private IBankContext context;

        public IRepository<Customer> Customers { get; set; }
        public IRepository<AccessCard> AccessCards { get; set; }
        public IRepository<AccessCode> AccessCodes { get; set; }
        public IRepository<OwnSSISPayment> OwnSSISPayments { get; set; }
        public IRepository<OwnMobilePayment> OwnMobilePayments { get; set; }
        public IRepository<OwnArbitraryPayment> OwnArbitraryPayments { get; set; }
        public IRepository<OwnTransferPayment> OwnTransferPayments { get; set; }
        public IRepository<SSISPayment> SSISPayments { get; set; }
        public IRepository<SSISGroup> SSISGroups { get; set; }
        public IRepository<MobileAutoPay> MobileAutoPayments { get; set; }

        public Repositories()
        {            
            this.context = new IBankContext();
            Customers = new DataRepository<Customer>(context);
            AccessCards = new DataRepository<AccessCard>(context);
            AccessCodes = new DataRepository<AccessCode>(context);
            OwnSSISPayments = new DataRepository<OwnSSISPayment>(context);
            OwnTransferPayments = new DataRepository<OwnTransferPayment>(context);
            OwnArbitraryPayments = new DataRepository<OwnArbitraryPayment>(context);
            OwnMobilePayments = new DataRepository<OwnMobilePayment>(context);
            SSISPayments = new DataRepository<SSISPayment>(context);
            SSISGroups = new DataRepository<SSISGroup>(context);
            MobileAutoPayments = new DataRepository<MobileAutoPay>(context);
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
