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
        public ActionResult UserInfo(string user = null, bool reloadSubscription = false, bool reloadSubscribers = false)
        {
            if(reloadSubscription)
            {
                ViewBag.ReloadSubscription = true;
            }

            if(reloadSubscribers)
            {
                ViewBag.ReloadSubscribers = true;
            }

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
            if (ModelState.IsValid)
            {
                List<string> tags = new List<string>();

                foreach (var tag in System.Text.RegularExpressions.Regex.Matches(model.Text, @"#\w+"))
                {
                    tags.Add(tag.ToString());
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
            else
            {
                return View(model);
            }
        }

        public ActionResult MakeRepost(int id)
        {
            if (User == null)
            {
                return new HttpStatusCodeResult(400);
            }

            var mes = messageService.GetMessageById(id);

            if (mes == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (userService.GetUserNameById(mes.UserId) == User.Identity.Name)
                {
                    return new HttpStatusCodeResult(400);
                }
            }

            if (userService.IsRepostAlreadyMade(User.Identity.Name, id))
            {
                return new HttpStatusCodeResult(400);
            }

            var message = messageService.GetMessageById(id);

            List<string> tags = new List<string>();

            foreach (var tag in System.Text.RegularExpressions.Regex.Matches(message.Text, @"#\w+"))
            {
                tags.Add(tag.ToString());
            }

            userService.AddNewMessage(
                new BLL.DTOModels.DTOMessage
                {
                    Text = message.Text,
                    Tags = tags,
                    DateTime = DateTime.Now,
                    UserId = userService.GetUserIdByName(User.Identity.Name),
                    OriginalMessageId = message.Id
                });

            return RedirectToAction("UserMessages");
        }
            
        public ActionResult DeleteMessage(int id)
        {
            var message = messageService.GetMessageById(id);

            if (message == null)
            {
                return new HttpStatusCodeResult(400);
            }

            if (message.UserId != userService.GetUserIdByName(User.Identity.Name))
            {
                return new HttpStatusCodeResult(400);
            }

            userService.DeleteMessage(id);

            return new HttpStatusCodeResult(200);
        }

        [AllowAnonymous]
        public ActionResult UserMessages(string user = null)
        {
            if (user == null)
            {
                if (User != null && User.Identity.IsAuthenticated)
                {

                    ViewBag.IsItMe = true;

                    var dtoMessages = messageService.GetAllUserMessages(userService.GetUserIdByName(User.Identity.Name));

                    List<Twits.WEB.Models.UserMessage> messages = new List<Models.UserMessage>();

                    foreach(var mes in dtoMessages)
                    {
                        mes.RepostCount = messageService.GetRepostCount(mes.Id);

                        Models.UserMessage message = mes.ToWeb();

                        if(mes.OriginalMessageId !=null)
                        {                            
                            var originalMessage = messageService.GetMessageById(mes.OriginalMessageId??0);

                            if (originalMessage != null)
                            {
                                message.OriginalMessage = new Models.UserMessage
                                {
                                    Id = originalMessage.Id,
                                    DateTime = originalMessage.DateTime,
                                    Login = originalMessage.Login,
                                    Text = originalMessage.Text
                                };
                            }
                        }

                        messages.Add(message);
                    }

                    return PartialView(messages.OrderByDescending(m=>m.DateTime));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (User != null && User.Identity.IsAuthenticated && User.Identity.Name == user)
                {
                    ViewBag.IsItMe = true;
                }

                var dtoMessages = messageService.GetAllUserMessages(userService.GetUserIdByName(user));

                List<Twits.WEB.Models.UserMessage> messages = new List<Models.UserMessage>();

                foreach (var mes in dtoMessages)
                {
                    mes.RepostCount = messageService.GetRepostCount(mes.Id);

                    Models.UserMessage message = mes.ToWeb();

                    if (mes.OriginalMessageId != null)
                    {
                        var originalMessage = messageService.GetMessageById(mes.OriginalMessageId ?? 0);

                        if (originalMessage != null)
                        {
                            message.OriginalMessage = new Models.UserMessage
                            {
                                Id = originalMessage.Id,
                                DateTime = originalMessage.DateTime,
                                Login = userService.GetUserNameById(originalMessage.UserId),
                                Text = originalMessage.Text
                            };
                        }
                    }

                    messages.Add(message);
                }

                return PartialView(messages.OrderByDescending(m=>m.DateTime));
            }
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if(User!=null && User.Identity.IsAuthenticated)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        [HttpPost]
        public ActionResult UploadAvatar()
        {
            HttpPostedFileBase file = Request.Files[0];

            if(file == null || file.FileName == "")
            {
                ViewBag.ErrorMessage = "Вы попытались загрузить пустой файл!";
                return View("Edit");
            }

            if (file.ContentLength >= 10000000)
            {
                ViewBag.ErrorMessage = "Файл слишком большой (попробуйте меньше 5-7 мб)";
                return View("Edit");
            }

            switch (file.ContentType)
            {
                case "image/jpeg":
                    break;
                case "image/png":
                    break;
                default:
                    ViewBag.ErrorMessage = "Неверный формат";
                    return View("Edit");
            }

            string noavPath = Server.MapPath("~/Images/Avatars/noav.png");

            System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);

            string filename = Server.MapPath("~/Images/Avatars/av" + User.Identity.Name + ".png");
            
            img.Save(filename, System.Drawing.Imaging.ImageFormat.Png);            

            file.InputStream.Dispose();

            return RedirectToAction("UserInfo", "User");
        }
    }
}