using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twits.WEB.CustomMappers;

namespace Twits.WEB.Controllers
{
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {
        private BLL.Interfaces.IUser userService;
        private BLL.Interfaces.IMessage messageService;

        public UserController(BLL.Interfaces.IUser iUser, BLL.Interfaces.IMessage iMessage)
        {
            userService = iUser;
            messageService = iMessage;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserInfo(string user)
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult AddNewMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddNewMessage(Models.NewMessage model)
        {
            List<string> tags = new List<string>();

            foreach(var word in model.Text.Split(' '))
            {
                if(word.Contains('#'))
                {
                    tags.Add(System.Text.RegularExpressions.Regex.Replace(word, @"^\#\w+$", ""));
                }
            }

            BLL.DTOModels.DTOMessage message = new BLL.DTOModels.DTOMessage
            {
                DateTime = DateTime.Now,
                Tags = tags,
                Text = model.Text,
                UserId = userService.GetUserIdByName(User.Identity.Name)
            };

            userService.AddNewMessage(message);

            return RedirectToAction("UserMessages");
        }

        public ActionResult UserMessages()
        {
            return PartialView(messageService.GetAllUserMessages(userService.GetUserIdByName(User.Identity.Name)).ToWeb());
        }
    }
}