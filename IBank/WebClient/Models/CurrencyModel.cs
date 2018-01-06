using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class CurrencyModel
    {
        public List<CurrencyItem> Rates { get; set; }
        public Dictionary<string, string> CrossRates { get; set; }

        public CurrencyModel()
        {
            Rates = new List<CurrencyItem>();
            CrossRates = new Dictionary<string, string>();
        }
    }
}