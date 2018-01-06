using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OwnSSISPayment
    {
        public int OwnSSISPaymentID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public int SSISPaymentID { get; set; }
        public virtual SSISPayment SSISPayment { get; set; }
        public int CardAccountID { get; set; }
    }
}
