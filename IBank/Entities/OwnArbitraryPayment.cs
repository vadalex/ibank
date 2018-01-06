using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OwnArbitraryPayment
    {
        public int OwnArbitraryPaymentID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string UNP { get; set; }
        public string Recipient { get; set; }
        public string RecipientAccount { get; set; }
        public string BankCode { get; set; }
        public string Target { get; set; }
        public int CardAccountID { get; set; }
    }
}
