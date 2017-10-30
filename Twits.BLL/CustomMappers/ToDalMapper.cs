using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.BLL.CustomMappers
{
    public static class ToDalMapper
    {
        public static DAL.Models.User ToDal(this BLL.DTOModels.UserDTO user)
        {
            return new DAL.Models.User
            {
                Login = user.Login,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }

        public static DAL.Models.Message ToDal(this BLL.DTOModels.DTOMessage message)
        {
            return new DAL.Models.Message
            {
                OriginalMessageId = message.OriginalMessageId,
                DateTime = message.DateTime,
                Text = message.Text,
                UserId = message.UserId
            };
        }
    }
}
