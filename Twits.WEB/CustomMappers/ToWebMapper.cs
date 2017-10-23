using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twits.WEB.CustomMappers
{
    public static class ToWebMapper
    {
        public static WEB.Models.UserMessage ToWeb(this BLL.DTOModels.DTOViewMessage dtoMessage)
        {
            return new Models.UserMessage
            {
                DateTime = dtoMessage.DateTime,
                Text = dtoMessage.Text,
                Id = dtoMessage.Id,
                Login = dtoMessage.Login
            };
        }

        public static IEnumerable<WEB.Models.UserMessage> ToWeb(this IEnumerable<BLL.DTOModels.DTOViewMessage> dtoList)
        {
            List<Models.UserMessage> messages = new List<Models.UserMessage>();

            foreach(var mes in dtoList)
            {
                messages.Add(mes.ToWeb());
            }

            return messages.AsEnumerable();
        }
    }
}