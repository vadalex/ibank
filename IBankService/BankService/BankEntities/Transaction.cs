using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankEntities
{
    [DataContract]
    public enum PaymentType
    {
        [EnumMember]
        Arbitrary,
        [EnumMember]
        SSIS,
        [EnumMember]
        Transfer,
        [EnumMember]
        Mobile
    }

    
    public interface ITransaction
    {        
        DateTime Date { get; set; }
        double Amount { get; set; }
        PaymentType Type { get; set; }
        int CustomerID { get; set; }
        int CardAccountID { get; set; }
    }
}
