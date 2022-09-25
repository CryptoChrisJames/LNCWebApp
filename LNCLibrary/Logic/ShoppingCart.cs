using LNCLibrary.Logic;
using LNCLibrary.Models;
using LNCLibrary.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNCLibrary.Models
{
    public partial class ShoppingCart
    {
        //Class properties-->
        private readonly ApplicationDbContext _context;

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ShoppingCartID { get; set; }

        List<CartItems> ShoppingCartItems { get; set; }
        //<-- end Class properties

        //Getting the cart 
        public List<CartItems> GetCart(string CartID)
        {
            if(ShoppingCartID != CartID)
            {
                ShoppingCartID = CartID;
            }
            
            if (_context.CartItems.Any(c => c.cartID == CartID))
            {
                ShoppingCartItems = (from c in _context.CartItems
                                     where c.cartID == ShoppingCartID
                                     select new CartItems
                                     {
                                         ID = c.ID,
                                         cartID = c.cartID,
                                         name = c.name,
                                         price = c.price,
                                         itempicture = c.itempicture,
                                         productID = c.productID,
                                     }
                           ).ToList();
            }
                return ShoppingCartItems;
        }

        /// <summary>
        /// Adding items to cart
        /// </summary>
        /// <param name="CartItem"></param>
        /// <returns></returns>
        public async Task<List<CartItems>> AddToCart(CartItems CartItem)
        {
            _context.CartItems.Add(CartItem);
            await _context.SaveChangesAsync();
            ShoppingCartItems = GetCart(CartItem.cartID);
            return ShoppingCartItems;
        }

        public async Task<List<CartItems>> RemoveFromCart(RemovableItem RI)
        {
            CartItems item = new CartItems();
            item.ID = RI.cartitemid;
            _context.CartItems.Attach(item);
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            ShoppingCartItems = GetCart(RI.cartid);
            return ShoppingCartItems;
        }

        public async Task<List<CartItems>> EmptyCart(string cartid)
        {
            _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.cartID == cartid));
            await _context.SaveChangesAsync();
            ShoppingCartItems = GetCart(cartid);
            return ShoppingCartItems;
        }

        public int Total(List<CartItems> currentCart)
        {
            int Total = 0;
            foreach(var item in currentCart)
            {
                Total += item.price;
            }
            return Total;
        }

        public int StripeTotal(int Total)
        {
            int StripeTotal = Total * 100;
            return StripeTotal;
        }

        //// Come back when authentication is implemented.
        //// When a user has logged in, migrate their shopping cart to
        //// be associated with their username

        public async Task<List<CartItems>> MigrateCart(List<CartItems> currentCart, string userId)
        {
            ShoppingCartItems = currentCart;
            ShoppingCartID = userId;
            foreach(CartItems item in ShoppingCartItems)
            {
                item.cartID = ShoppingCartID;
            }

            await _context.SaveChangesAsync();
            return currentCart;

            //var shoppingCart = storeDB.Carts.Where(
            //    c => c.CartId == ShoppingCartId);

            //foreach (Cart item in shoppingCart)
            //{
            //    item.CartId = userName;
            //}
            //storeDB.SaveChanges();
        }
    }
}