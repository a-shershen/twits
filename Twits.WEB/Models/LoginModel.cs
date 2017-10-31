using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Twits.WEB.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Поле обязательно для заполнения")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Password { get; set; }
    }
}