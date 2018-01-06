using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SSISGroup
    {
        public int SSISGroupID { get; set; }
        public string GroupName { get; set; }
        public int ParentGroupID { get; set; }        
    }
}
