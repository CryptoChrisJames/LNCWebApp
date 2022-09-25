using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNCDemo.HomeViewModels
{
    public class CheckoutViewModel
    {
        public GuestCheckoutViewModel GCVM { get; set; }
        public OrderViewModel OVM { get; set; }
    }
}
