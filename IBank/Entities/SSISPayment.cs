using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class SSISPayment
    {
        public SSISPayment()
        {
            OwnSSISPayments = new HashSet<OwnSSISPayment>();
        }
        public int SSISPaymentID { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string NumberLabel { get; set; }
        public string Group { get; set; }
        public int GroupID { get; set; }        
        public string SSISNumber { get; set; }
        public string Regex { get; set; }
        public string Error { get; set; }
        public ICollection<OwnSSISPayment> OwnSSISPayments { get; set; }
    }
}
