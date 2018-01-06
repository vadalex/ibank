using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Name")]
        [RegularExpression(@"(?=^.{8,32}$)(?!.*\s)[0-9A-z_-]*$", ErrorMessage = "Логин может содержать только латинские буквы и цифры, дефис и символ подчеркивания. Длина от 8 до 32")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=^.{8,32}$)(?!.*\s)[0-9A-z]*$", ErrorMessage = "Пароль может содержать только латинские буквы и цифры. Длина от 8 до 32")]
        public string Password { get; set; }
    }

    public class SecretCodeModel
    {
        public int CodeIndex { get; set; }

        [Required(ErrorMessage = "Введите секретный код!")]
        [RegularExpression(@"(?=^.{4,4}$)(?!.*\s)[0-9]*$", ErrorMessage = "Секретный код - четырехзначное число")]
        public string EnteredCode { get; set; }
        public int Remaining { get; set; }
    }

    public class PasswordResetModel
    {
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=^.{8,32}$)(?!.*\s)[0-9A-z]*$", ErrorMessage = "Пароль может содержать только латинские буквы и цифры. Длина от 8 до 32")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=^.{8,32}$)(?!.*\s)[0-9A-z]*$", ErrorMessage = "Пароль может содержать только латинские буквы и цифры. Длина от 8 до 32")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=^.{8,32}$)(?!.*\s)[0-9A-z]*$", ErrorMessage = "Пароль может содержать только латинские буквы и цифры. Длина от 8 до 32")]
        public string ConfirmPassword { get; set; }
    }
}