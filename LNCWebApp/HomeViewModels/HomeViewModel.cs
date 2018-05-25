using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary.Models.HomeViewModel
{
    public class HomeViewModel
    {
        public ICollection<Product> ShopProducts { get; set; }
        public Cart CurrentCart { get; set; }
    }
}
