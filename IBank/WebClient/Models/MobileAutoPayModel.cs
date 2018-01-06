using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities;

namespace WebClient.Models
{
    public class MobileAutoPayModel
    {
        public int MobileAutoPayID { get; set; }

        public int CardAccountID { get; set; }

        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Введите название оператора")]
        public String Operator { get; set; }


        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Номер телефона должен состоять из девяти цифр (+375*********)")]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Введите сумму")]
        [RegularExpression(@"^\d+(\.(\d{0}))?$", ErrorMessage = "Сумма должна быть числом")]
        public double Amount { get; set; }


        [Range(0, 59, ErrorMessage = "Значение \"Минуты\" должно находиться в диапазоне от 0 до 59\n")]
        public int IntervalMinutes { get; set; }


        [Range(0, 23, ErrorMessage = "Значение \"Часы\" должно находиться в диапазоне от 0 до 23\n")]
        public int IntervalHours { get; set; }


        [Range(0, 9999, ErrorMessage = "Значение \"Дни\" должно быто положительно\n")]
        public int IntervalDays { get; set; }


        [Range(0, 59, ErrorMessage = "Значение \"Минуты\" должно находиться в диапазоне от 0 до 59\n")]
        public int StartMinutes { get; set; }


        [Range(0, 23, ErrorMessage = "Значение \"Часы\" должно находиться в диапазоне от 0 до 23\n")]
        public int StartHours { get; set; }


        [Required(ErrorMessage = "Введите время первого платежа\n")]
        public DateTime StartDate { get; set; }

        public DateTime LastPayDate { get; set; }
    }
}