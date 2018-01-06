using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankEntities
{
    [DataContract]
    public class Employee
    {        
        public Employee()
        {
            IsAdmin = false;
        }

        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }        
    }
}
