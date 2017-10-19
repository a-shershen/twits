using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.Interfaces
{
    public interface IAccount
    {
        void RegisterNewUser(DTOModels.UserDTO user);

        bool IsPasswordTrue(string login, string password);

        bool IsUserInRole(string login, string roleName);

        string GetUserRole(string login);

        int GetUserRoleId(string login);
    }
}
