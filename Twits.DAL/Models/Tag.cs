using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twits.DAL.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public int MessageId { get; set; }

        [ForeignKey("MessageId")]
        public Message Message { get; set; } 
    }
}
