using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MobileAutoPay
    {
        public int MobileAutoPayID { get; set; }
        public int PayCardId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastExecutionDate { get; set; }
        public TimeSpan Interval { get; set; }
        public int CustomerID { get; set; }
        public string MobileOperator { get; set; }
        public string MobileNumber { get; set; }
        public double Amount { get; set; }
    }
}
