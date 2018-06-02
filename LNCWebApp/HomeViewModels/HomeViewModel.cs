﻿using LNCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary.Models.HomeViewModel
{
    public class HomeViewModel
    {
        public ICollection<Product> ShopProducts { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public string SessionID { get; set; }
        public List<CartItems> currentCart { get; set; }
    }
}
