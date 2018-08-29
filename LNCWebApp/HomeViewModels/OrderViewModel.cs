﻿using LNCLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNCWebApp.HomeViewModels
{
    public class OrderViewModel
    {
        public Order CurrentOrder { get; set; }
        public string CartID { get; set; }
        public List<CartItems> CurrentCart { get; set; }
        public int TotalForStripe { get; set; }
    }
}
