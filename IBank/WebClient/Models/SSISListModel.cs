using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace WebClient.Models
{
    public class SSISListModel
    {
        public Stack<SSISGroup> GroupsPath { get; set; } 
        public IEnumerable<SSISListItemModel> Items { get; set; }
    }
}