using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Data;

namespace SuperMarketComparison.Controllers
{
    public class CartsController : Controller
    {
        private readonly SMCContext _context;
        public CartsController( SMCContext context )
        {
            _context = context;
        }

        // INDEX ./carts
        // view INDEX /carts
        public async Task<IActionResult> Index()
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ItemStorePrice)
                        .ThenInclude(isp => isp.Item)
                            .ThenInclude(i => i.Prices)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ItemStorePrice)
                        .ThenInclude(isp => isp.Store)
                .FirstOrDefaultAsync();

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

            ViewBag.totalMin = totalMin;
            ViewBag.totalMax = totalMax;
            
            return View(cart);
        }

        // ADD ./carts
        // view ADD /carts/add
        public IActionResult Add()
        {
            var item = _context.Items.ToList();

            ViewBag.ItemList = new SelectList(item, "Id", "Name");

            return View();
        }
    }
}
