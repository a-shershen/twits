using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Twits.WEB.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина логина от 3 до 20 символов")]
        [RegularExpression(@"^[a-zA-Z][\w-]*", ErrorMessage = "Только латинские буквы, цифры, дефис и знак подчёркивания, первый символ - буква")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Пароль от 6 до 64 символов")]
        [RegularExpression(@"[\w-]*", ErrorMessage = "Латинские буквы, цифры, знак подчёркивания, дефис")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Пароль от 6 до 64 символов")]
        [RegularExpression(@"[\w-]*", ErrorMessage = "Латинские буквы, цифры, знак подчёркивания, дефис")]
        [Compare("Password",ErrorMessage = "Введённые пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}