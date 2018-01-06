using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OwnTransferPayment
    {
        public int OwnTransferPaymentID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string TargetCardNumber { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public int CardAccountID { get; set; }
    }
}
