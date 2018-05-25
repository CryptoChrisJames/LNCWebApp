using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public ICollection<Product> CartItems { get; set; }
    }
}
