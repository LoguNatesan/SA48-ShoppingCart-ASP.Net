using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.DatabaseDetails;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetails
        public ActionResult GetOrderDetails(string sessionId, int userId)
        {
            List<OrderDetails> orderlist = OrderDetailsData.AddtoOrderDetails(sessionId,userId);
            ViewData["orderlist"] = orderlist;
            ViewData["sessionId"] = sessionId;
            ViewData["userId"] = userId;
            return View();
        }
    }
}