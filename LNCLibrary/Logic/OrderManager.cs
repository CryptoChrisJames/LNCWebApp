using LNCLibrary.Models;
using LNCWebApp.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Order> GetOrderByID(int OrderID)
        {
            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.ID == OrderID);
            return order;
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

        public async Task<List<Order>> GetClosedOrders()
        {
            List<Order> Orders = await _context.Orders.
                Where(r => r.Status == Status.Closed).
                ToListAsync();
            return Orders;
        }

        public async Task<List<Order>> GetArchivedOrders()
        {
            List<Order> Orders = await _context.Orders.
                Where(r => r.Status == Status.Archived).
                ToListAsync();
            return Orders;
        }

        public List<OrderDetails> GetOrderDetails(int OrderID)
        {
            var OrderDetails = _context.OrderDetails.Where(d => d.OrderID == OrderID).ToList();
            return OrderDetails;
        }

        public async Task<string> ArchiveOrder(int OrderID)
        {
            var currentOrder = await GetOrderByID(OrderID);
            currentOrder.Status = Status.Archived;
            await _context.SaveChangesAsync();
            return "Ok";
        }

        public async Task<List<Order>> GetUserOrders(string id)
        {
            List<Order> Orders = await _context.Orders.
                Where(r => r.CartID == id && r.Status == Status.Closed || r.Status == Status.Archived).
                ToListAsync();
            return Orders;
        }
    }
}
