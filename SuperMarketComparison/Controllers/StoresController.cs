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

        // INDEX
        // view INDEX
        public async Task<IActionResult> Index()
        {
            var store = await _context.Stores.ToListAsync();
            return View(store);
        }

        // CREATE
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

        // EDIT
        // view Edit
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            return View(store);
        }
        // update db UPDATE store
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Location")] Store store)
        {
            if ( ModelState.IsValid )
            {
                _context.Stores.Update(store);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        // DELETE
        // view DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == id);
            return View(store);
        }
        // update db DELETE store
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
