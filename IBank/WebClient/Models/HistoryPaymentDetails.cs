using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using WebClient.BankService;

namespace WebClient.Models
{
    public class HistoryPaymentDetails
    {
        public Dictionary<string, string> Dictionary { get; set; }
        public RouteValueDictionary BackRouteValues { get; set; }
    }
}