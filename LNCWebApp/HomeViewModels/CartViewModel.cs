using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNCLibrary.Models;

namespace LNCWebApp.HomeViewModels
{
    public class CartViewModel
    {
        public List<CartItems> currentCart { get; set; }
        public int currentPrice { get; set; }
    }
}
