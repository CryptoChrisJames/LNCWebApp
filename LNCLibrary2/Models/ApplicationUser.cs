using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LNCLibrary2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LNCLibrary2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public USStates State { get; set; }
        public string CheckoutComments { get; set; }
        public int ZipCode { get; set; }
        public List<Order> OrderHistory { get; set; }
    }
}
