using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BankEntities
{
    [DataContract]
    public class Currency
    {
        [DataMember]
        public int CurrencyID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public double Rate { get; set; }
    }
}
