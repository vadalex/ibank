using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankEntities
{
    [DataContract]
    public class SSISTransaction : ITransaction
    {
        public SSISTransaction()
        {
            Type = PaymentType.SSIS;
            Date = Time.GetTime();
        }

        [DataMember]
        public int SSISTransactionID { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public PaymentType Type { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public int CardAccountID { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public int SSISPaymentID { get; set; }
    }
}
