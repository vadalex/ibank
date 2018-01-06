using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AccessCard
    {
        public AccessCard()
        {
            AccessCodes = new HashSet<AccessCode>();
        }
        public int AccessCardID { get; set; }
        public int EnteredCodeFail { get; set; }
        public int CustomerID { get; set; }
        public bool IsBlocked { get; set; }
        public Customer Customer { get; set; }
        public ICollection<AccessCode> AccessCodes { get; set; }
    }
}
