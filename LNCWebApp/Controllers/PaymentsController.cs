using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LNCLibrary.Models;
using LNCWebApp.Data;
using LNCWebApp.HomeViewModels;
using LNCWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace LNCWebApp.Controllers
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
        public IActionResult Index(string CartID)
        {
            return View();
        }
        
    }
}
