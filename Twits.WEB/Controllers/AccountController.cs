using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twits.WEB.Controllers
{
    public class AccountController : Controller
    {
        private BLL.Interfaces.IAccount accountService;

        public AccountController(BLL.Interfaces.IAccount accountService)
        {
            this.accountService = accountService;
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginModel model)
        {
            ViewBag.LoginError = false;

            if(ModelState.IsValid)
            {
                if (accountService.IsPasswordTrue(model.Login, Hash.Hash.GetHash(model.Password)))
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(model.Login, true);

                    return RedirectToAction("UserInfo", "User", new { user = model.Login });
                }
                else
                {
                    ViewBag.LoginError = true;

                    return View(model);
                }
            }
            else
            {
                ViewBag.LoginError = true;

                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                accountService.RegisterNewUser(new BLL.DTOModels.UserDTO
                {
                    Login = model.Login,
                    Password = Hash.Hash.GetHash(model.Password),
                    RoleId = accountService.GetRoleIdByName("user")
                });

                return RedirectToAction("UserInfo", "User", new { user = model.Login }); 
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}