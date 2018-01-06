using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.BankService;

namespace WebClient.Models
{
    public class HistoryItemModel
    {
        public int TransactionId { get; set; }
        public PaymentType Type { get; set; }
        public string CardNumber { get; set; }
        public DateTime Date { get; set; }
        public string Recipient { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}