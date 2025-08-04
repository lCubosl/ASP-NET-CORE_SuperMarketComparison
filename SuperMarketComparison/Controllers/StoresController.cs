using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Data;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Controllers
{
    public class StoresController : Controller
    {
        private readonly SMCContext _context;
        public StoresController( SMCContext context )
        {
            _context = context;
        }

        // view INDEX
        public async Task<IActionResult> Index()
        {
            var store = await _context.Stores.ToListAsync();
            return View(store);
        }

        // view CREATE
        public IActionResult Create()
        {
            return View();
        }
        // update db CREATE new store
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Location")] Store store)
        {
            if(ModelState.IsValid)
            {
                _context.Stores.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Items");
            }
            return View(store);
        }
    }
}
