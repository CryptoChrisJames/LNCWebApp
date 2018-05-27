using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LNCLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LNCWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Cart")]
        public int CartID { get; set; }
        public Cart Cart { get; set; }
        [ForeignKey("Order")]
        public virtual ICollection<int> OrderIDs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
