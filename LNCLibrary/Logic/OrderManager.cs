using LNCLibrary.Models;
using LNCWebApp.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LNCLibrary.Logic
{
    public class OrderManager
    {
        private readonly ApplicationDbContext _context;
        public string CartID { get; set; }
        public Order CurrentOrder { get; set; }

        public OrderManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order GetOrder(string CartID)
        {
            Order currentOrder = new Order();

            currentOrder = _context.Orders.FirstOrDefault(o => o.CartID == CartID);

            return currentOrder;
        }

        public async Task<Order> GetNewOrder(string CartID, int OrderID)
        {
            Order currentOrder = new Order();

            currentOrder = _context.Orders.Where(o => o.CartID == CartID)
                .Where(o => o.ID == OrderID).FirstOrDefault();

            return await (ConvertCartToOrder(currentOrder));
        }

        public async Task<Order> ConvertCartToOrder(Order currentOrder)
        {
            ShoppingCart _shoppingCart = new ShoppingCart(_context);
            List<CartItems> currentCart = new List<CartItems>();
            List<OrderDetails> currentOrderDetails = new List<OrderDetails>();
            currentOrder.OrderDetails = currentOrderDetails;
            currentCart = _shoppingCart.GetCart(CartID);


            foreach(var item in currentCart)
            {
                OrderDetails OD = new OrderDetails();
                OD.CartID = item.cartID;
                OD.Name = item.name;
                OD.OrderID = currentOrder.ID;
                OD.price = item.price;
                OD.productpicture = item.itempicture;
                _context.OrderDetails.Add(OD);
            }

            currentOrder.OrderDetails = currentOrderDetails;
            await _context.SaveChangesAsync();
            return currentOrder;
        }
    }
}
