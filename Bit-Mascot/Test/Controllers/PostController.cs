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
    public class PostController : Controller
    {
        // GET: Post
        private readonly IPostManager postManager;
        private readonly IActivityManager activityManager;
        public PostController()
        {
            postManager = new PostManager();
            activityManager = new ActivityManager();
        }
        public ActionResult Index()
        {
            if (Session["CurrentUser"]== null) {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetAllPosts(int pageNo, string searcText, int entries)
        {
            var data = postManager.GetAllPosts(pageNo, searcText, entries);
            return Json(data, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult ToggleCommentActivity(Activity activity)
        {
            activity.UserID = Session["CurrentUser"].ToString();
            var data = activityManager.ToggleCommentActivity(activity);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}