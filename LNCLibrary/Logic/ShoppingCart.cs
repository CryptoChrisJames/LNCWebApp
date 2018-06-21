using LNCLibrary.Logic;
using LNCLibrary.Models;
using LNCWebApp.Data;
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

        //public void EmptyCart()
        //{
        //    var cartItems = storeDB.Carts.Where(
        //        cart => cart.CartId == ShoppingCartId);

        //    foreach (var cartItem in cartItems)
        //    {
        //        storeDB.Carts.Remove(cartItem);
        //    }
        //    // Save changes
        //    storeDB.SaveChanges();
        //}
        //public List<Cart> GetCartItems()
        //{
        //    return storeDB.Carts.Where(
        //        cart => cart.CartId == ShoppingCartId).ToList();
        //}
        //public int GetCount()
        //{
        //    // Get the count of each item in the cart and sum them up
        //    int? count = (from cartItems in storeDB.Carts
        //                  where cartItems.CartId == ShoppingCartId
        //                  select (int?)cartItems.Count).Sum();
        //    // Return 0 if all entries are null
        //    return count ?? 0;
        //}
        //public decimal GetTotal()
        //{
        //    // Multiply album price by count of that album to get 
        //    // the current price for each of those albums in the cart
        //    // sum all album price totals to get the cart total
        //    decimal? total = (from cartItems in storeDB.Carts
        //                      where cartItems.CartId == ShoppingCartId
        //                      select (int?)cartItems.Count *
        //                      cartItems.Album.Price).Sum();

        //    return total ?? decimal.Zero;
        //}
        //public int CreateOrder(Order order)
        //{
        //    decimal orderTotal = 0;

        //    var cartItems = GetCartItems();
        //    // Iterate over the items in the cart, 
        //    // adding the order details for each
        //    foreach (var item in cartItems)
        //    {
        //        var orderDetail = new OrderDetail
        //        {
        //            AlbumId = item.AlbumId,
        //            OrderId = order.OrderId,
        //            UnitPrice = item.Album.Price,
        //            Quantity = item.Count
        //        };
        //        // Set the order total of the shopping cart
        //        orderTotal += (item.Count * item.Album.Price);

        //        storeDB.OrderDetails.Add(orderDetail);

        //    }
        //    // Set the order's total to the orderTotal count
        //    order.Total = orderTotal;

        //    // Save the order
        //    storeDB.SaveChanges();
        //    // Empty the shopping cart
        //    EmptyCart();
        //    // Return the OrderId as the confirmation number
        //    return order.OrderId;
        //}
        //// We're using HttpContextBase to allow access to cookies.
        //public string GetCartId(HttpContextBase context)
        //{
        //    if (context.Session[CartSessionKey] == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
        //        {
        //            context.Session[CartSessionKey] =
        //                context.User.Identity.Name;
        //        }
        //        else
        //        {
        //            // Generate a new random GUID using System.Guid class
        //            Guid tempCartId = Guid.NewGuid();
        //            // Send tempCartId back to client as a cookie
        //            context.Session[CartSessionKey] = tempCartId.ToString();
        //        }
        //    }
        //    return context.Session[CartSessionKey].ToString();
        //}
        //// When a user has logged in, migrate their shopping cart to
        //// be associated with their username
        //public void MigrateCart(string userName)
        //{
        //    var shoppingCart = storeDB.Carts.Where(
        //        c => c.CartId == ShoppingCartId);

        //    foreach (Cart item in shoppingCart)
        //    {
        //        item.CartId = userName;
        //    }
        //    storeDB.SaveChanges();
        //}
    }
}