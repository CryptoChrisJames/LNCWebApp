using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LNCLibrary.Models
{
    public class CartItems
    {
        public int ID { get; set; }
        public string cartID { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string itempicture { get; set; }
        public int productID { get; set; }

    }
}
