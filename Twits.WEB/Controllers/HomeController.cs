using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twits.WEB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult TopPanel()
        {
            if(Request.IsAuthenticated)
            {
                return PartialView("UserTopPanel", User.Identity.Name);
            }
            else
            {
                return PartialView("AccountTopPanel");
            }
        }
    }
}