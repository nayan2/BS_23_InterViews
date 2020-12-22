using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Core.Bll.Interfaces;
using Test.Core.Bll.Managers;
using Test.Core.Model;

namespace Test.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly IUserManager userManager;

        public LoginController()
        {
            userManager = new UserManager();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginUser(string username,string password) {
            var user=userManager.Get(username, password);
            if (user != null)
            {                
                Session["CurrentUser"] = user.UserID;
            }
            else
            {
                Session["CurrentUser"] =null;
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            Session["CurrentUser"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}