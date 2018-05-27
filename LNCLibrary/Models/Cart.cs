using LNCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public virtual ICollection<CartItems> Itinerary { get; set; }
        public string UserID { get; set; }

    }
}
