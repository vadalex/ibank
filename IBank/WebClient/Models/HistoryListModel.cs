using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebClient.Models
{
    public class HistoryListModel
    {
        public RouteValueDictionary BackRouteValues { get; set; }
        public IEnumerable<HistoryItemModel> Items { get; set; } 
    }
}