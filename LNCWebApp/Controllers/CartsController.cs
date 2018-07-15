using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LNCLibrary.Models;
using LNCWebApp.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using LNCWebApp.Models;
using LNCWebApp.HomeViewModels;
using LNCLibrary.Logic;

namespace LNCWebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Carts")]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _currentUser;

        public CartsController(ApplicationDbContext context, UserManager<ApplicationUser> currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Route("ShowCartPopup")]
        public PartialViewResult ShowCartPopup(string cartID)
        {
            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            List<CartItems> currentCart = _shoppingCart.GetCart(cartID);
            return PartialView("~/Views/Carts/_CartPartial.cshtml", currentCart);
        }

        [HttpPost]
        [Route("AddToCart")]
        public Task<List<CartItems>> AddToCart(CartItems CartItem)
        {

            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            return  _shoppingCart.AddToCart(CartItem);
        }

        [HttpPost]
        [Route("RemoveItem")]
        public async Task<PartialViewResult> RemoveItem(RemovableItem RI)
        {
            ShoppingCart shoppingCart = new ShoppingCart(_context);
            List<CartItems> updatdedCart = await shoppingCart.RemoveFromCart(RI);
            return PartialView("~/Views/Carts/_CartPartial.cshtml", updatdedCart);
        }

        
        [HttpPost]
        [Route("EmptyCart")]
        public async Task<PartialViewResult> EmptyCart(string cartid)
        {
            ShoppingCart shoppingCart = new ShoppingCart(_context);
            List<CartItems> emptiedcart = await shoppingCart.EmptyCart(cartid);
            return PartialView("~/Views/Carts/_CartPartial.cshtml", emptiedcart);
        }

        [HttpPost]
        [Route("Checkout")]
        public IActionResult Checkout (string cartid)
        {
            CartViewModel CVM = new CartViewModel();
            ShoppingCart shoppingCart = new ShoppingCart(_context);
            CVM.CartID = cartid;
            CVM.CurrentCart = shoppingCart.GetCart(CVM.CartID);
            CVM.NumberOfItems = CVM.CurrentCart.Count;
            CVM.CartTotal = shoppingCart.Total(CVM.CurrentCart);
            return View(CVM);
        }

        [HttpPost]
        [Route("RemoveItemAtCheckout")]
        public async Task<IActionResult> RemoveItemAtCheckout(string cartid,int cartitemid)
        {
            RemovableItem RI = new RemovableItem();
            RI.cartid = cartid;
            RI.cartitemid = cartitemid;
            ShoppingCart shoppingCart = new ShoppingCart(_context);
            List<CartItems> updatdedCart = await shoppingCart.RemoveFromCart(RI);
            CartViewModel CVM = new CartViewModel();
            CVM.CartID = RI.cartid;
            CVM.CurrentCart = shoppingCart.GetCart(CVM.CartID);
            CVM.NumberOfItems = CVM.CurrentCart.Count;
            CVM.CartTotal = shoppingCart.Total(CVM.CurrentCart);
            return View("~/Views/Carts/Checkout.cshtml", CVM);
        }

        [HttpPost]
        [Route("GuestCheckout")]
        public PartialViewResult GuestCheckout(string CartID)
        {
            GuestCheckoutViewModel GCVM = new GuestCheckoutViewModel();
            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            GCVM.GuestCart = _shoppingCart.GetCart(CartID);
            GCVM.GuestOrder = new Order();
            GCVM.CartID = CartID;
            return PartialView("~/Views/Carts/GuestOrderCreation.cshtml", GCVM);
        }
    }
}