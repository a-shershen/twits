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

        [AllowAnonymous]
        public ActionResult UserInfo(string user = null)
        {
            if(user == null)
            {
                if(User!=null && User.Identity.IsAuthenticated)
                {
                    ViewBag.NotMyPage = false;
                    ViewBag.UserName = User.Identity.Name;

                    return View();
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            else
            {
                if(User!=null && User.Identity.Name==user)
                {
                    ViewBag.NotMyPage = false;
                    ViewBag.UserName = User.Identity.Name;

                    return View();
                }

                else
                {
                    ViewBag.NotMyPage = true;
                    ViewBag.UserName = user;

                    if (User.Identity.IsAuthenticated)
                    {
                        ViewBag.IsSubscribed = userService.IsSubscribed(User.Identity.Name, user);
                    }

                    return View();
                }
            }
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

        [AllowAnonymous]
        public ActionResult UserMessages(string user = null)
        {
            if (user == null)
            {
                if (User != null && User.Identity.IsAuthenticated)
                {

                    return PartialView(messageService.GetAllUserMessages(userService.GetUserIdByName(User.Identity.Name)).ToWeb());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return PartialView(messageService.GetAllUserMessages(userService.GetUserIdByName(user)).ToWeb());
            }
        }
    }
}