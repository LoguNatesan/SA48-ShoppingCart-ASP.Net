﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class User
    {
        public int UserId
        {
            get;set;
        }

        public string UserName
        {
            get; set;
        }

        public string FullName
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string SessionId
        {
            get; set;
        }
    }
}