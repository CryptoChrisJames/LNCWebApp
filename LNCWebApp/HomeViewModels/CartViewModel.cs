using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNCLibrary2.Models;

namespace LNCWebApp.HomeViewModels
{
    public class CartViewModel
    {
        public List<CartItems> CurrentCart { get; set; }
        public string SessionID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string CartID { get; set; }
        public int NumberOfItems { get; set; }
        public int CartTotal { get; set; }
    }
}
