using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class AccessCode
    {
        public int AccessCodeID { get; set; }
        public int Number { get; set; }
        public string Code { get; set; }
        public int AccessCardID { get; set; }
        public virtual AccessCard AccessCard { get; set; }
    }
}
