using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.DTOModels
{
    public class UserDTO
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }
    }
}
