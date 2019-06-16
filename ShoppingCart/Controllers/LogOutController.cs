using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.DatabaseDetails;

namespace ShoppingCart.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult UserLogOut(string sessionId,int UserId)
        {
            UserData.RemoveSession(UserId);
            OrderDetailsData.RemoveSessionInOrder(UserId);
            return View();
        }
    }
}