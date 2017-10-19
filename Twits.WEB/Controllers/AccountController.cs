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
            if(ModelState.IsValid)
            {
                if (accountService.IsPasswordTrue(model.Login, Hash.Hash.GetHash(model.Password)))
                {
                    return RedirectToAction("UserInfo", "User", new { user = model.Login });
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
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
                    RoleId = accountService.GetUserRoleId(model.Login)
                });

                return RedirectToAction("UserInfo", "User", new { user = model.Login }); 
            }
            else
            {
                return View(model);
            }
        }



    }
}