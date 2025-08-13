using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Data;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Controllers
{
    public class CartsController : Controller
    {
        private readonly SMCContext _context;
        public CartsController(SMCContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ToListAsync();

            return View(carts);
        }

        // DETAILS ./carts/details
        // view DETAILS /carts/details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ItemStorePrice)
                        .ThenInclude(isp => isp.Item)
                            .ThenInclude(i => i.Prices)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ItemStorePrice)
                        .ThenInclude(isp => isp.Store)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
                return NotFound();

            decimal totalMin = 0;
            decimal totalMax = 0;

            foreach (var cartItem in cart.CartItems)
            {
                var item = cartItem.ItemStorePrice?.Item;

                if (item?.Prices != null && item.Prices.Any())
                {
                    totalMin += item.Prices.Min(p => p.Price);
                    totalMax += item.Prices.Max(p => p.Price);
                }
            }

            cart.MinEstPrice = totalMin;
            cart.MaxEstPrice = totalMax;
            
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            ViewBag.totalMin = totalMin;
            ViewBag.totalMax = totalMax;

            return View(cart);
        }

        // ADD ./carts
        // view ADD /carts/add
        [HttpGet("Carts/Add/{cartId}")]
        public async Task<IActionResult> Add(int cartId)
        {
            var item = await _context.Items.ToListAsync();
            if (item == null)
                return NotFound();

            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null) 
                return NotFound();

            ViewBag.ItemList = new SelectList(item, "Id", "Name");
            ViewBag.CartId = cart.Id;

            return View();
        }

        // ADD CREATE new instance in db cartitems
        [HttpPost]
        public async Task<IActionResult> Add(int cartId, int itemId)
        {
            if (itemId == 0)
                // status code 400
                return BadRequest("Item must be selecterd");

            var cart = await _context.Carts.FindAsync(cartId);
            if(cart == null)
                return NotFound();

            var price = await _context.ItemStorePrices
                .Where(p => p.ItemId == itemId)
                .OrderBy(p => p.Price)
                .FirstOrDefaultAsync();

            if (price == null)
            {
                return RedirectToAction(
                    "Create",
                    "ItemStorePrices",
                    new {id = itemId}
                );
            }

            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                ItemStorePriceId = price.Id,
                IsChecked = false
            };

            _context.CartItems.Add(newCartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Carts", new {id = cart.Id});
        }

        // REMOVE instance in db cartitems
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // CREATE instance in db Cart
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var item = new Cart();
            _context.Carts.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // REMOVE instance in db Cart
        [HttpPost, ActionName("DeleteCart")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var item = await _context.Carts.FindAsync(id);
            if (item != null)
            {
                _context.Carts.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // UPDATE instance in Cart -> UPDATES CompletedAt
        [HttpPost]
        public async Task<IActionResult> MarkAsBought(int id, decimal actualPrice)
        {
            var item = _context.Carts.Find(id);
            if (item == null)
                return NotFound();

            item.CompletedAt = DateTime.UtcNow;
            item.ActualPrice = actualPrice;

            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
