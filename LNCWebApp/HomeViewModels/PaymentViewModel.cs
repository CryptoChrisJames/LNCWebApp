using LNCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNCWebApp.HomeViewModels
{
    public class PaymentViewModel
    {
        public Order CurrentOrder { get; set; }
        public List<CartItems> CurrentCart { get; set; }
        public string CurrentCartId { get; set; }
    }
}
