using LNCLibrary2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary2.Models.HomeViewModel
{
    public class HomeViewModel
    {
        public ICollection<Product> ShopProducts { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public string SessionID { get; set; }
        public List<CartItems> currentCart { get; set; }
        public string ShopID { get; set; }
    }
}
