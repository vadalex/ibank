using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BankEntities
{

    public static class Time
    {
        public static DateTime GetTime()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. Europe Standard Time").AddHours(1);
        }
    }

    [DataContract]
    public class ArbitraryTransaction : ITransaction
    {
        public ArbitraryTransaction()
        {
            Type = PaymentType.Arbitrary;
            Date = Time.GetTime();
        }
        [DataMember]
        public int ArbitraryTransactionID { get; set; }
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
        public string UNP { get; set; }
        [DataMember]
        public string Recipient { get; set; }
        [DataMember]
        public string RecipientAccount { get; set; }
        [DataMember]
        public string BankCode { get; set; }
        [DataMember]
        public string Target { get; set; }
    }
}
