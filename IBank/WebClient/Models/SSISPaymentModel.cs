using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class SSISPaymentModel
    {
        public int SSISPaymentID { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string NumberLabel { get; set; }
        [Required(ErrorMessage = "Это поле нужно заполнить")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Поле должно состоять из цифр")]
        public string Number { get; set; }
        public string Group { get; set; }
        public int CardAccountID { get; set; }
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Сумма - обязательное поле")]
        [RegularExpression(@"^\d+(\.(\d{1,2}))?$", ErrorMessage = "Сумма должна быть числом (небольше двух знаков после точки)")]
        public double Amount { get; set; }        
        public bool ToOwnPayments { get; set; }
        public bool FromOwnPayments { get; set; }
    }
}