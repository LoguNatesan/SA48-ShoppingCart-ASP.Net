﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class CartDetails
    {
        public int UserId
        {
            get;set;
        }

        public int ProductId
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public string SessionId
        {
            get;set;
        }

        public GalleryDetails Gallery
        {
            get;set;
        }
    }
}