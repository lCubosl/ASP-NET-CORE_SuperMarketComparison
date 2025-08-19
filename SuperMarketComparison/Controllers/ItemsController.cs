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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = sortOrder switch
            {
                "name_asc" => "name_desc",
                "name_desc" => "",
                _ => "name_asc"
            };

            var item = _context.Items
                .Include(i => i.Prices)
                .ThenInclude(isp => isp.Store)
                .AsQueryable();

            // filtering
            if(!string.IsNullOrEmpty(searchString))
            {
                item = item.Where(d => d.Name.Contains(searchString));
                return View(await item.ToListAsync());
            }

            // sorting
            switch (sortOrder)
            {
                case "name_asc":
                    item = item.OrderBy(i => i.Name); 
                    break;
                case "name_desc":
                    item = item.OrderByDescending(i => i.Name);
                    break;
                default:
                    break;
            }

            return View(await item.ToListAsync());
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
            var item = await _context.Items
                .Include(i => i.Prices)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                _context.ItemStorePrices.RemoveRange(item.Prices);
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
