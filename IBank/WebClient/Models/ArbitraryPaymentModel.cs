using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class ArbitraryPaymentModel
    {
        
        [Required(ErrorMessage = "Сумма - обязательное поле")]
        [RegularExpression(@"^\d+(\.(\d{1,2}))?$", ErrorMessage="Сумма должна быть числом (небольше двух знаков после точки)")]
        public double Amount { get; set; }
        public string UNP { get; set; }
        [Required(ErrorMessage = "Получатель - обязательное поле")]
        public string Recipient { get; set; }
        [Required(ErrorMessage = "Номер счета получателя - обязательное поле")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Номер счета получателя должен состоять из цифр")]
        public string RecipientAccount { get; set; }
        [Required(ErrorMessage = "Банк получатель - обязательное поле")]
        public string BankName { get; set; }
        public string Target { get; set; }
        public int AccountCardId { get; set; }
        public string AccountCardNumber { get; set; }
        public bool ToOwnPayments { get; set; }
        public bool FromOwnPayments { get; set; }
    }
}