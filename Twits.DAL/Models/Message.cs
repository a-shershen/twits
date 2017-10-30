using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twits.DAL.Models
{
    public class Message
    {
        public Message()
        {
            Tags = new List<Models.Tag>();
        }

        public int Id { get; set; }

        public int? OriginalMessageId { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Models.Tag> Tags { get; set; }
    }
}
