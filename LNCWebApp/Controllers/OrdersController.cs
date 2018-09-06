using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LNCLibrary.Models;
using LNCWebApp.Data;
using LNCLibrary.Logic;

namespace LNCWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            OrderManager OM = new OrderManager(_context);
            return View(await OM.GetClosedOrders());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var OM = new OrderManager(_context);
            var order = await OM.GetOrderByID(id);
            order.OrderDetails = OM.GetOrderDetails(order.ID);
            return View(order);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> ArchiveDetails(int id)
        {
            var OM = new OrderManager(_context);
            var order = await OM.GetOrderByID(id);
            order.OrderDetails = OM.GetOrderDetails(order.ID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FinalPrice,ConfirmationNumber,Status,isGuest,DateOfPurchase,CartID,PaymentMethod,FirstName,LastName,Address,City,State,Email,CheckoutComments,ZipCode")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveOrder (int OrderID)
        {
            var OM = new OrderManager(_context);
            string status = await OM.ArchiveOrder(OrderID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Archive()
        {
            OrderManager OM = new OrderManager(_context);
            return View(await OM.GetArchivedOrders());
        }
    }
}
