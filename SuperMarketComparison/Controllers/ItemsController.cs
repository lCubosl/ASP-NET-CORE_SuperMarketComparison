using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Data;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Controllers
{
    public class ItemsController : Controller
    {
        private readonly SMCContext _context;
        public ItemsController(SMCContext context )
        {
            _context = context;
        }

        // ./items/{id}
        // view DETAILS /items/{id}
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Items
                .Include(i => i.Prices)
                .ThenInclude(isp => isp.Store)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (item == null)
                return NotFound();

            return View(item);
        }

        // INDEX
        // view INDEX
        public async Task<IActionResult> Index()
        {
            var item = await _context.Items
                .Include (i => i.Prices)
                .ToListAsync();
            return View(item);
        }
        // view CREATE
        public IActionResult Create()
        {
            return View();
        }

        // CREATE
        // update DB CREATE new item
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Description")] Item item)
        {
            if(ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        
        // EDIT
        // view EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        // update DB UPDATE item
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // DELETE
        // view DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        //update DB DELETE item
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
