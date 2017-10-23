using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twits.WEB.Controllers
{
    public class SubscriptionController : Controller
    {
        private BLL.Interfaces.IUser userService;

        public SubscriptionController(BLL.Interfaces.IUser iUser)
        {
            userService = iUser;
        }

        public PartialViewResult GetSubscriptions(string user)
        {
            return PartialView(userService.GetSubscriptions(user));
        }

        [Authorize(Roles ="user")]
        public ActionResult Subscribe(string subscription)
        {
            userService.Subscribe(User.Identity.Name, subscription);

            return RedirectToAction("UserInfo", "User", new { user = subscription });
        }


        [Authorize(Roles = "user")]
        public ActionResult Unsubscribe(string subscription)
        {
            userService.Unsubscribe(User.Identity.Name, subscription);

            return RedirectToAction("UserInfo", "User", new { user = subscription });
        }

        public PartialViewResult GetSubscribers(string user)
        {
            return PartialView(userService.GetSubscribers(user));
        }


    }
}