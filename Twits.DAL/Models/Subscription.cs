using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twits.DAL.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ReadUserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ReadUserId")]
        public virtual User ReadUser { get; set; }
    }
}
