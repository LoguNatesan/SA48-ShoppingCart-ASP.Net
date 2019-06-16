using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.DatabaseDetails;

namespace ShoppingCart.Controllers
{
    public class GalleryDetailsController : Controller
    {
        // GET: ProductDetails
        public ActionResult GetGallery(string sessionId, int userId)
        {
            List<GalleryDetails> products = GalleryDetailsData.GetGalleryProductDetails(sessionId);
            User user = UserData.GetUserDetailsBySessionId(sessionId);

            ViewData["products"] = products;
            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;
            ViewData["count"] = CartData.GetCartCount(sessionId,userId);
            //return RedirectToAction("GetGalleryBySearch","GalleryDetails",new {sessionId});
            return View();
        }

        public ActionResult GetGalleryBySearch(string sessionId, string ProductName,int UserId)
        {
            List<GalleryDetails> products = GalleryDetailsData.GetSearchProductDetails(ProductName);
            User user = UserData.GetUserDetailsBySessionId(sessionId);
            ViewData["products"] = products;
            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;
            ViewData["count"] = CartData.GetCartCount(sessionId,UserId);
            return View("GetGallery");
        }

        public ActionResult AddToMyPurchases(string sessionId, int UserId)
        {
            //List <OrderDetails>= GalleryDetailsData.AddtoMyPurchases(sessionId,UserId);
            return RedirectToAction("GetGallery", "GalleryDetails", new { sessionId,UserId });
        }
    }
}