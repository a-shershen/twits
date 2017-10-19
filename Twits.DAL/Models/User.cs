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

            SubscriptionsUsers = new List<Models.Subscription>();


            SubscriptionsReadUsers = new List<Models.Subscription>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Models.Message> Messages { get; set; }

        public virtual ICollection<Models.Subscription> SubscriptionsUsers { get; set; }

        public virtual ICollection<Models.Subscription> SubscriptionsReadUsers { get; set; }
    }
}
