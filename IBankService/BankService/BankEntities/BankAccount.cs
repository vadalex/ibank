using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankEntities
{
    [DataContract]
    public class BankAccount
    {
        public BankAccount()
        {
            CreatedDate = Time.GetTime();
            IsLocked = false;
        }

        [DataMember]
        public int BankAccountID { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public double Balance { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
        [DataMember]
        public string AcountNumber { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public int CurrencyID { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
