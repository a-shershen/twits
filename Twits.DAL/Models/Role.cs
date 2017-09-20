using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.DAL.Models
{
    public class Role
    {
        public Role()
        {
            Users = new List<Models.User>();
        }

        public int Id { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<Models.User> Users { get; set; }
    }
}
