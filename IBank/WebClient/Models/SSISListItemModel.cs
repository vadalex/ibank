using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public enum SSISListItemType
    {
        Group, Payment
    }

    public class SSISListItemModel
    {
        public SSISListItemType Type { get; set; }
        public int Id { get; set; }
        public string Label { get; set; }
    }
}