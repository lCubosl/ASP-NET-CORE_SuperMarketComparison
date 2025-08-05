using Microsoft.AspNetCore.Mvc;
using SuperMarketComparison.Data;
using SuperMarketComparison.Models;
using System.Threading.Tasks;

namespace SuperMarketComparison.Controllers
{
    public class ItemStorePricesController : Controller
    {
        private readonly SMCContext _context;
        public ItemStorePricesController(SMCContext context )
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int id)
        {
            var item = await _context.Items.FindAsync(id);
            
            if (item == null)
                return NotFound();

            var model = new ItemStorePrice
            {
                ItemId = id,
                Item = item,
                LastUpdate = DateTime.Today
            };

            return View(model);
        }
    }
}
