using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twits.WEB.Controllers
{
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {
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
        public PartialViewResult AddNewMessage(Models.NewMessage model)
        {
            return null;
        }
    }
}