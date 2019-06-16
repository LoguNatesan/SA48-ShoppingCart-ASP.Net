using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.DatabaseDetails;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult UserLogin()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        public ActionResult UserLogin(string UserName,string Password)
        {
            if(UserName == null)
                return View();

            User user = UserData.GetUserDetails(UserName);
            if (Password != user.Password)
                return View();

            string SessionId = UserData.CreateSession(user.UserId);
            ViewData["sessionId"]= SessionId;
            ViewData["userId"] = user.UserId;
            return RedirectToAction("GetGallery","GalleryDetails",new { SessionId,userId=user.UserId });

        }

        public ActionResult UserLogout(int UserId)
        {
            UserData.RemoveSession(UserId);
            return RedirectToAction("UserLogin", "Login");
        }
    }
}