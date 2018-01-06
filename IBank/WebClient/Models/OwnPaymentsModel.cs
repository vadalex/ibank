using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace WebClient.Models
{
    public class OwnPaymentsModel
    {
        public IEnumerable<OwnArbitraryPayment> Arbitrary { get; set; }
        public IEnumerable<OwnMobilePayment> Mobile { get; set; }
        public IEnumerable<OwnTransferPayment> Transfer { get; set; }
        public IEnumerable<OwnSSISPayment> SSIS { get; set; }
    }
}