using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LNCLibrary.Data;
using LNCLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using LNCDemo.Models.HomeViewModel;

namespace LNCDemo.Controllers
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
            if (User.Identity.IsAuthenticated)
            {
                HVM.CurrentUser = await _currentUser.GetUserAsync(User);
                HVM.currentCart = _shoppingCart.GetCart(HVM.CurrentUser.Id);
                HVM.ShopID = HVM.CurrentUser.Id;
                if(HVM.currentCart != null)
                {
                    HVM.currentCart = await _shoppingCart.EmptyCart(HVM.ShopID);
                }

                return View(HVM);
            }
            else
            {
                 
                HVM.currentCart = _shoppingCart.GetCart(HVM.SessionID);
                HVM.ShopID = HVM.SessionID;
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
