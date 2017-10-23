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
            return PartialView(messageService.GetLastMessages(10).ToWeb());
        }

        public PartialViewResult GetFeed(string user)
        {
            return PartialView(messageService.GetFeed(userService.GetUserIdByName(user)).ToWeb());
        }
    }
}