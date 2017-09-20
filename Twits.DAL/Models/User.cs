using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twits.DAL.Models
{
    public class User
    {
        public User()
        {
            Messages = new List<Models.Message>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Models.Message> Messages { get; set; }
    }
}
