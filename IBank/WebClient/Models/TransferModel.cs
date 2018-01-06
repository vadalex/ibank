using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class TransferModel
    {
        [Required(ErrorMessage = "Сумма - обязательное поле")]
        [RegularExpression(@"^\d+(\.(\d{1,2}))?$", ErrorMessage = "Сумма должна быть числом (небольше двух знаков после точки)")]
        public double Amount { get; set; }
        public string Message { get; set; }
        [Required(ErrorMessage = "Номер карты получателя - обязательное поле")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Номер карты должен состоять из шестнадцати цифр")]
        public string TargetCardAccountNumber { get; set; }
        public int TargetCardAccountId { get; set; }
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public bool ToOwnPayments { get; set; }
        public bool FromOwnPayments { get; set; }
    }
}