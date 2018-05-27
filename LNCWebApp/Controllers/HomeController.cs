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
            HVM.CurrentCart = new LNCLibrary.Models.Cart();
            HVM.ShopProducts = await _context.Products.ToListAsync();
            return View(HVM);
        }

        [HttpPost]
        public JsonResult AddToCart(CartItems item)
        {
            
            return Json("");
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
