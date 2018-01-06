using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OwnMobilePayment
    {
        public int OwnMobilePaymentID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string MobileProvider { get; set; }
        public int CardAccountID { get; set; }
        
    }
}
