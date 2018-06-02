using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LNCLibrary.Models;
using LNCWebApp.Data;

namespace LNCWebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Carts")]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddToCart")]
        public async Task<List<CartItems>> AddToCart(CartItems CartItem)
        {
            _context.Add(CartItem);
            await _context.SaveChangesAsync();
            List<CartItems> currentCart = new List<CartItems>();
            //currentCart = (from c in _context.CartItems
            //               where c.cartID == CartItem.cartID
            //               select new CartItems
            //               {
            //                   ID = c.ID,
            //                   cartID = c.cartID,
            //                   name = c.name,
            //                   price = c.price,
            //                   itempicture = c.itempicture,
            //                   productID = c.productID,
            //               }
            //               ).ToList();
            return (currentCart);
        }

        //        // GET: api/Carts/5
        //        [HttpGet("{id}")]
        //        public async Task<IActionResult> GetCart([FromRoute] string id)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            var cart = await _context.Carts.SingleOrDefaultAsync(m => m.ID == id);

        //            if (cart == null)
        //            {
        //                return NotFound();
        //            }

        //            return Ok(cart);
        //        }

        //        // PUT: api/Carts/5
        //        [HttpPut("{id}")]
        //        public async Task<IActionResult> PutCart([FromRoute] string id, [FromBody] Cart cart)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            if (id != cart.ID)
        //            {
        //                return BadRequest();
        //            }

        //            _context.Entry(cart).State = EntityState.Modified;

        //            try
        //            {
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!CartExists(id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }

        //            return NoContent();
        //        }

        //        // POST: api/Carts
        //        [HttpPost]
        //        public async Task<IActionResult> PostCart([FromBody] Cart cart)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            _context.Carts.Add(cart);
        //            await _context.SaveChangesAsync();

        //            return CreatedAtAction("GetCart", new { id = cart.ID }, cart);
        //        }

        //        // DELETE: api/Carts/5
        //        [HttpDelete("{id}")]
        //        public async Task<IActionResult> DeleteCart([FromRoute] string id)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return BadRequest(ModelState);
        //            }

        //            var cart = await _context.Carts.SingleOrDefaultAsync(m => m.ID == id);
        //            if (cart == null)
        //            {
        //                return NotFound();
        //            }

        //            _context.Carts.Remove(cart);
        //            await _context.SaveChangesAsync();

        //            return Ok(cart);
        //        }

        //        private bool CartExists(string id)
        //        {
        //            return _context.Carts.Any(e => e.ID == id);
        //        }
    }
}