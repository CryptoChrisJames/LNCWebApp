using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LNCWebApp.Data;
using LNCWebApp.Models;
using Microsoft.EntityFrameworkCore;
using LNCLibrary.Models.HomeViewModel;
using Microsoft.AspNetCore.Identity;
using LNCLibrary.Models;
using Microsoft.AspNetCore.Http;

namespace LNCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _currentUser;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel HVM = new HomeViewModel();
            HVM.SessionID = HttpContext.Session.Id;
            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            HVM.ShopProducts = await _context.Products.ToListAsync();
            if (User.Identity.IsAuthenticated == true)
            {
                HVM.CurrentUser = await _currentUser.GetUserAsync(User);
                HVM.currentCart = _shoppingCart.GetCart(HVM.CurrentUser.UserName);
                return View(HVM);
            }
            else
            {
                 
                HVM.currentCart = _shoppingCart.GetCart(HVM.SessionID);
                return View(HVM);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
