using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class MobilePaymentModel
    {
        public string Provider { get; set; }
        [Required(ErrorMessage = "Номер телефона - обязательное поле")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Номер телефона должен состоять из девяти цифр (+375*********)")]
        public string Phone { get; set; }
        public int CardAccountID { get; set; }
        [RegularExpression(@"^\d+(\.(\d{1,2}))?$", ErrorMessage = "Сумма должна быть числом (небольше двух знаков после точки)")]
        [Required(ErrorMessage = "Сумма - обязательное поле")]
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public bool ToOwnPayments { get; set; }
        public bool FromOwnPayments { get; set; }
    }
}