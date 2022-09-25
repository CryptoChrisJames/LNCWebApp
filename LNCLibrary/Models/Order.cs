using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LNCLibrary.Models
{
    public enum USStates
    {
        [Description("Alabama")]
        AL,

        [Description("Alaska")]
        AK,

        [Description("Arkansas")]
        AR,

        [Description("Arizona")]
        AZ,

        [Description("California")]
        CA,

        [Description("Colorado")]
        CO,

        [Description("Connecticut")]
        CT,

        [Description("D.C.")]
        DC,

        [Description("Delaware")]
        DE,

        [Description("Florida")]
        FL,

        [Description("Georgia")]
        GA,

        [Description("Hawaii")]
        HI,

        [Description("Iowa")]
        IA,

        [Description("Idaho")]
        ID,

        [Description("Illinois")]
        IL,

        [Description("Indiana")]
        IN,

        [Description("Kansas")]
        KS,

        [Description("Kentucky")]
        KY,

        [Description("Louisiana")]
        LA,

        [Description("Massachusetts")]
        MA,

        [Description("Maryland")]
        MD,

        [Description("Maine")]
        ME,

        [Description("Michigan")]
        MI,

        [Description("Minnesota")]
        MN,

        [Description("Missouri")]
        MO,

        [Description("Mississippi")]
        MS,

        [Description("Montana")]
        MT,

        [Description("North Carolina")]
        NC,

        [Description("North Dakota")]
        ND,

        [Description("Nebraska")]
        NE,

        [Description("New Hampshire")]
        NH,

        [Description("New Jersey")]
        NJ,

        [Description("New Mexico")]
        NM,

        [Description("Nevada")]
        NV,

        [Description("New York")]
        NY,

        [Description("Oklahoma")]
        OK,

        [Description("Ohio")]
        OH,

        [Description("Oregon")]
        OR,

        [Description("Pennsylvania")]
        PA,

        [Description("Rhode Island")]
        RI,

        [Description("South Carolina")]
        SC,

        [Description("South Dakota")]
        SD,

        [Description("Tennessee")]
        TN,

        [Description("Texas")]
        TX,

        [Description("Utah")]
        UT,

        [Description("Virginia")]
        VA,

        [Description("Vermont")]
        VT,

        [Description("Washington")]
        WA,

        [Description("Wisconsin")]
        WI,

        [Description("West Virginia")]
        WV,

        [Description("Wyoming")]
        WY
    }
    public enum Status
    {
        Open,
        Archived,
        Refunded,
        Closed
    }
    public enum PaymentMethod
    {
        Stripe
    }
    public class Order
    {
        //Data that will be manipulated on the server-side, 
        //primarily for processing purposes. 
        //This data will not come in to contact with the user.

        public int ID { get; set; }
        public float FinalPrice { get; set; }
        public string ConfirmationNumber { get; set; }
        public Status Status { get; set; }
        public bool isGuest { get; set; }
        public ApplicationUser RegularCustomer { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string CartID { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = (PaymentMethod)1;

        //Data that will need to be supplied by the user in order to complete the order. 

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public USStates State { get; set; }
        public string Email { get; set; }
        public string CheckoutComments { get; set; }
        public int ZipCode { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
