using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twits.BLL.DTOModels;

using Twits.BLL.CustomMappers;

namespace Twits.BLL.Services
{
    public class AccountService : Interfaces.IAccount
    {
        private DAL.Interfaces.IUnitOfWork db;

        public AccountService(DAL.Interfaces.IUnitOfWork db)
        {
            this.db = db;
        }

        public bool IsPasswordTrue(string login, string password)
        {
            if(db.Users.Read(u=>u.Login==login && u.Password == password) != null)
            {
                return true;
            }
            
            else
            {
                return false;
            }
        }

        public bool IsUserInRole(string login, string roleName)
        {
            var role = db.Roles.Read(r => r.RoleName == roleName);

            if (role != null)
            {
                var user = db.Users.Read(u => u.Login == login && u.RoleId == role.Id);

                if (user != null)
                {
                    return true;
                }
            }

            return false;
        }

        public void RegisterNewUser(UserDTO user)
        {
            db.Users.Create(user.ToDal());
        }
    }
}
