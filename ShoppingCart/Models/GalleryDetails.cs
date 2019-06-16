using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class GalleryDetails
    {
        public int ProductId
        {
            get;set;
        }

        public string ProductName
        {
            get;set;
        }

        public string Description
        {
            get;set;
        }

        public int Price
        {
            get;set;
        }

        public string ImageUrl
        {
            get;set;
        }
    }
}