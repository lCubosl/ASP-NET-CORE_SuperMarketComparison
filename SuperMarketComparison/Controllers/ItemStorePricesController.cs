using Microsoft.AspNetCore.Mvc;
using SuperMarketComparison.Data;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Controllers
{
    public class ItemStorePricesController : Controller
    {
        private readonly SMCContext _context;
        public ItemStorePricesController(SMCContext context )
        {
            _context = context;
        }

        public IActionResult Create(int id)
        {
            var item = new ItemStorePrice
            {
                Id = id
            };

            return View();
        }
    }
}
