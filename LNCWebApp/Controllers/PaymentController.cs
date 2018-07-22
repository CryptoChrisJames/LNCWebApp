using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNCWebApp.Data;
using LNCWebApp.HomeViewModels;
using LNCWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LNCWebApp.Controllers
{
    public class PaymentController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public IActionResult Index(string CartID)
        {
            CartID = (string)TempData["cartid"];
            return View();
        }
    }
}