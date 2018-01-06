using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.BankService;

namespace WebClient.Models
{
    public class CardAccountModel
    {
        public CardAccount CardAccount { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }
        public string CardNumber { get; set; }
    }
}