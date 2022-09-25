using LNCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNCDemo.HomeViewModels
{
    public class GuestCheckoutViewModel
    {
        public Order GuestOrder { get; set; }
        public string CartID { get; set; }
        public List<CartItems> CurrentCart { get; set; }
        public int TotalForStripe { get; set; }
    }
}
