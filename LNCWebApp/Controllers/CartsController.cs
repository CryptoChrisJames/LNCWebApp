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

        public CartsController(ApplicationDbContext context,
            UserManager<ApplicationUser> currentUser)
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
            return _shoppingCart.AddToCart(CartItem);
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
        public IActionResult Checkout(string cartid)
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
        public async Task<IActionResult> RemoveItemAtCheckout(string cartid, int cartitemid)
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
            GCVM.GuestOrder = new Order();
            GCVM.CartID = CartID;
            GCVM.CurrentCart = _shoppingCart.GetCart(CartID);
            GCVM.GuestOrder.FinalPrice = _shoppingCart.Total(GCVM.CurrentCart);
            GCVM.TotalForStripe = _shoppingCart.StripeTotal((int)GCVM.GuestOrder.FinalPrice);
            return PartialView("~/Views/Carts/GuestOrderCreation.cshtml", GCVM);
        }

        [HttpPost]
        [Route("GuestOrderCreation")]
        public async Task<IActionResult> GuestOrderCreation(GuestCheckoutViewModel GCVM)
        {
            GCVM.GuestOrder.CartID = GCVM.CartID;
            GCVM.GuestOrder.Status = Status.Open;
            GCVM.GuestOrder.isGuest = true;
            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            GCVM.CurrentCart = _shoppingCart.GetCart(GCVM.CartID);
            GCVM.GuestOrder.FinalPrice = _shoppingCart.Total(GCVM.CurrentCart);
            GCVM.TotalForStripe = _shoppingCart.StripeTotal((int)GCVM.GuestOrder.FinalPrice);
            await _context.Orders.AddAsync(GCVM.GuestOrder);
            await _context.SaveChangesAsync();
            CheckoutViewModel CVM = new CheckoutViewModel();
            CVM.GCVM = GCVM;
            return View("~/Views/Carts/ConfirmOrder.cshtml", CVM);
        }

        [HttpPost]
        [Route("OrderCreation")]
        public async Task<IActionResult> OrderCreation(string cartid)
        {
            ApplicationUser currentUser = await _currentUser.GetUserAsync(User);
            if (currentUser.Id == cartid)
            {
                OrderViewModel OVM = new OrderViewModel();
                Order newOrder = new Order();
                OVM.CartID = cartid;
                OVM.CurrentOrder = newOrder;
                OVM.CurrentOrder.FirstName = currentUser.FirstName;
                OVM.CurrentOrder.LastName = currentUser.LastName;
                OVM.CurrentOrder.Address = currentUser.Address;
                OVM.CurrentOrder.City = currentUser.City;
                OVM.CurrentOrder.State = currentUser.State;
                OVM.CurrentOrder.Email = currentUser.Email;
                OVM.CurrentOrder.ZipCode = currentUser.ZipCode;
                OVM.CurrentOrder.CheckoutComments = currentUser.CheckoutComments;
                OVM.CurrentOrder.CartID = OVM.CartID;
                OVM.CurrentOrder.Status = Status.Open;
                OVM.CurrentOrder.isGuest = false;
                ShoppingCart _shoppingCart = new ShoppingCart(_context);
                OVM.CurrentCart = _shoppingCart.GetCart(OVM.CartID);
                OVM.CurrentOrder.FinalPrice = _shoppingCart.Total(OVM.CurrentCart);
                OVM.TotalForStripe = _shoppingCart.StripeTotal((int)OVM.CurrentOrder.FinalPrice);
                await _context.Orders.AddAsync(OVM.CurrentOrder);
                await _context.SaveChangesAsync();
                CheckoutViewModel CVM = new CheckoutViewModel();
                CVM.OVM = OVM;
                return View("~/Views/Carts/ConfirmOrder.cshtml", CVM);
            }
            else
            {
                return View("Error");
            }

        }


        [HttpGet]
        [Route("LoginAtCheckout")]
        public async Task<IActionResult> LoginAtCheckout(string CartID)
        {
            ApplicationUser currentUser = await _currentUser.GetUserAsync(User);
            OrderViewModel OVM = new OrderViewModel();
            Order newOrder = new Order();
            OVM.CartID = CartID;
            OVM.CurrentOrder = newOrder;
            OVM.CurrentOrder.FirstName = currentUser.FirstName;
            OVM.CurrentOrder.LastName = currentUser.LastName;
            OVM.CurrentOrder.Address = currentUser.Address;
            OVM.CurrentOrder.City = currentUser.City;
            OVM.CurrentOrder.State = currentUser.State;
            OVM.CurrentOrder.Email = currentUser.Email;
            OVM.CurrentOrder.ZipCode = currentUser.ZipCode;
            OVM.CurrentOrder.CheckoutComments = currentUser.CheckoutComments;
            OVM.CurrentOrder.CartID = OVM.CartID;
            OVM.CurrentOrder.Status = Status.Open;
            OVM.CurrentOrder.isGuest = false;
            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            OVM.CurrentCart = _shoppingCart.GetCart(OVM.CartID);
            OVM.CurrentCart = await _shoppingCart.MigrateCart(OVM.CurrentCart, currentUser.Id);
            OVM.CurrentOrder.FinalPrice = _shoppingCart.Total(OVM.CurrentCart);
            OVM.TotalForStripe = _shoppingCart.StripeTotal((int)OVM.CurrentOrder.FinalPrice);
            await _context.Orders.AddAsync(OVM.CurrentOrder);
            await _context.SaveChangesAsync();
            CheckoutViewModel CVM = new CheckoutViewModel();
            CVM.OVM = OVM;
            return View("~/Views/Carts/ConfirmOrder.cshtml", CVM);
        }
    }
}