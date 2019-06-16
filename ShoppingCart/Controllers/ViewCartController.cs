using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.DatabaseDetails;
using System.Data.SqlClient;

namespace ShoppingCart.Controllers
{
    public class ViewCartController : Controller
    {
        // GET: ViewCart
        public ActionResult AddToCartDetails(string sessionId,int ProductId,int UserId,int Price)
        {
            CartDetails cart = new CartDetails();
            cart.ProductId = ProductId;
            cart.UserId = UserId;
            cart.SessionId = sessionId;
            cart.Price = Price;
            CartData.AddtoCartDetails(cart);
            return RedirectToAction("GetGallery","GalleryDetails",new {sessionId,userId=UserId});
        }

        public ActionResult AddToViewCart(string sessionId,int UserId)
        {
            List<CartDetails> gallerycart = CartData.AddtoViewCart(sessionId,UserId);
            ViewData["gallerycart"] = gallerycart;
            ViewData["sessionId"] = sessionId;
            ViewData["userId"] = UserId;
            return View();
        }

        public ActionResult UpdateCartDetails(CartDetails cart)
        {
           CartData.UpdateCartDetails(cart);

            return RedirectToAction("AddToViewCart","ViewCart",new { cart.SessionId,cart.UserId });
        }
    }
}