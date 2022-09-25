using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LNCLibrary.Models;
using LNCLibrary.Data;
using LNCDemo.HomeViewModels;
using Microsoft.AspNetCore.Identity;
using LNCLibrary.Logic;

namespace LNCDemo.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public PaymentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Payments
        public async Task<IActionResult >Index(string stripeEmail, string stripeToken, string CartID, int stripeTotal, int OrderID)
        {
            CashingOut CO = new CashingOut();
            if (CO.StripePayment(stripeEmail, stripeToken, stripeTotal))
            {
                OrderManager OM = new OrderManager(_context);
                OM.CartID = CartID;
                OM.CurrentOrder = await OM.GetNewOrder(OM.CartID, OrderID);
                OM.CurrentOrder.DateOfPurchase = DateTime.Now;
                OM.CurrentOrder.Status = Status.Closed;
                OM.CurrentOrder.ConfirmationNumber = CO.ConfirmationNumber();
                ShoppingCart emptyCart = new ShoppingCart(_context);
                await emptyCart.EmptyCart(CartID);
                return View(OM.CurrentOrder);
            }
            else
            {
                return View();
            }
        }
        
    }
}
