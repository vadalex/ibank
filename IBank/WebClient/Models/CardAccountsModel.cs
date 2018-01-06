using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class CardAccountsModel
    {
        public IEnumerable<CardAccountModel> CardAccounts { get; set; }
        public string SelectCardAccountId { get; set; }
    }
}