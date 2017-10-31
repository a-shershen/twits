using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Twits.WEB.Models
{
    public class NewMessage
    {
        [Required(ErrorMessage = "Сообщение не может быть пустым!")]
        [StringLength(140,ErrorMessage ="Длина сообщения не больше 140 символов!")]
        public string Text { get; set; }
    }
}