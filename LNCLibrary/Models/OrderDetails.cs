using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        public string CartID { get; set; }
        public int OrderID { get; set; }
        public string Name { get; set; }
        public string productpicture { get; set; }
        public int price { get; set; }
    }
}
