using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Twits.WEB.CustomMappers;

namespace Twits.WEB.Controllers
{
    public class MessageController : Controller
    {
        private BLL.Interfaces.IMessage messageService;
        private BLL.Interfaces.IUser userService;

        public MessageController(BLL.Interfaces.IMessage iMessage, BLL.Interfaces.IUser iUser)
        {
            messageService = iMessage;
            userService = iUser;
        }

        // GET: Message
        public PartialViewResult LastMessages()
        {
            var dtoMessages = messageService.GetLastMessages(10);

            List<Models.UserMessage> messages = new List<Models.UserMessage>();

            foreach(var dtoMes in dtoMessages)
            {
                dtoMes.RepostCount = messageService.GetRepostCount(dtoMes.Id);

                var mes = dtoMes.ToWeb();

                if (dtoMes.OriginalMessageId != null)
                {
                    var orMes = messageService.GetMessageById(dtoMes.OriginalMessageId ?? 0);

                    orMes.Login = userService.GetUserNameById(orMes.UserId);

                    mes.OriginalMessage = orMes.ToWeb();
                }

                messages.Add(mes);                
            }

            return PartialView(messages);
        }

        public PartialViewResult GetFeed(string user)
        {
            var dtoMessages = messageService.GetFeed(userService.GetUserIdByName(user));

            List<Models.UserMessage> messages = new List<Models.UserMessage>();

            foreach (var dtoMes in dtoMessages)
            {
                var mes = dtoMes.ToWeb();

                if (dtoMes.OriginalMessageId != null)
                {
                    var orMes = messageService.GetMessageById(dtoMes.OriginalMessageId ?? 0);

                    mes.OriginalMessage = orMes.ToWeb();
                }

                messages.Add(mes);
            }

            return PartialView(messages);
        }
    }
}