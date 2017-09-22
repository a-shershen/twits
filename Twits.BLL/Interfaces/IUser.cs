using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.Interfaces
{
    public interface IUser
    {
        void AddNewMessage(DTOModels.DTOMessage message);

        void Follow(int userId, int followUserId);
    }
}
