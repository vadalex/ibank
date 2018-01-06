using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Customer
    {
        public Customer()
        {
            OwnSSISPayments = new HashSet<OwnSSISPayment>();
            OwnMobilePayments = new HashSet<OwnMobilePayment>();
            OwnArbitraryPayments = new HashSet<OwnArbitraryPayment>();
            OwnTransferPayments = new HashSet<OwnTransferPayment>();
            AccessCards = new HashSet<AccessCard>();
        }
        public int CustomerID { get; set; }
        public string Login { get; set; }
        public string Passoword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string PassportNumber { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public virtual ICollection<OwnSSISPayment> OwnSSISPayments { get; set; }
        public virtual ICollection<OwnMobilePayment> OwnMobilePayments { get; set; }
        public virtual ICollection<OwnTransferPayment> OwnTransferPayments { get; set; }
        public virtual ICollection<OwnArbitraryPayment> OwnArbitraryPayments { get; set; }

        public virtual ICollection<AccessCard> AccessCards { get; set; }
    }
}