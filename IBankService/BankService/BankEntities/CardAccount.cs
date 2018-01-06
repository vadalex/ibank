using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BankEntities
{
    [DataContract]
    public class CardAccount
    {
        public CardAccount()
        {
            CreatedDate = Time.GetTime();
            IsLocked = false;
            IsDefault = false;
        }

        [DataMember]
        public int CardAccountID { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsLocked { get; set; }
        [DataMember]
        public bool IsDefault { get; set; }        
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public DateTime ExpiredDate { get; set; }
        [DataMember]
        public string CardType { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public int BankAccountID { get; set; }        

    }  
}
