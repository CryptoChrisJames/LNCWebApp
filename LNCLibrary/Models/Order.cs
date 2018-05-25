using System;
using System.Collections.Generic;
using System.Text;

namespace LNCLibrary.Models
{
    public enum Status
    {
        Open,
        Archived,
        Refunded,
        Closed
    }
    public class Order
    {
        public int ID { get; set; }
        public float FinalPrice { get; set; }
        public int ConfirmationNumber { get; set; }
        public Status Status { get; set; }
        public Cart Cart { get; set; }
    }
}
