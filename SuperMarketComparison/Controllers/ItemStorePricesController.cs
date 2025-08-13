using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        // CREATE
        // view CREATE /itemstoreprices/create/{id}
        [HttpGet("/ItemStorePrices/Create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var item = await _context.Items
                .Include(i => i.Prices)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            if (item == null)
                return NotFound();

            var usedStoreIds = item.Prices.Select(p => p.StoreId).ToList();

            var stores = await _context.Stores
                .Where(s => !usedStoreIds.Contains(s.Id))
                .ToListAsync();

            ViewBag.StoreList = new SelectList(stores, "Id", "Name");

            var model = new ItemStorePrice
            {
                ItemId = id,
                Item = item,
                LastUpdate = DateTime.Today
            };

            return View(model);
        }
        // update db CREATE new itemstoreprice
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ItemId, StoreId, Price, LastUpdate")] ItemStorePrice model)
        {
            if (ModelState.IsValid)
            {
                _context.ItemStorePrices.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Items", new { id = model.ItemId });
            }

            var stores = await _context.Stores.ToListAsync();
            ViewBag.StoreList = new SelectList(stores, "Id", "Name");

            return View(model);
        }

        // EDIT
        // view EDIT /itemstoreprices/edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.ItemStorePrices
                .Include(i => i.Item)
                .Include(i => i.Store)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }
        // update db EDIT itemstoreprice
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ItemId, StoreId, Price, LastUpdate")] ItemStorePrice model)
        {
            if (ModelState.IsValid)
            {
                _context.ItemStorePrices.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Items", new { id = model.ItemId });
            }

            var stores = await _context.Stores.ToListAsync();
            ViewBag.StoreList = new SelectList(stores, "Id", "Name");

            return View(model);
        }

        // UPDATE
        // view UPDATE /itemstoreprices/update/{id}
        public async Task<IActionResult> Update(int id)
        {
            var item = await _context.ItemStorePrices
                .Include(i => i.Item)
                .Include(i => i.Store)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }
        // update db UPDATE itemstoreprice

        // DELETE
        // view DELETE /itemstoreprices/delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ItemStorePrices
                .Include(i => i.Item)
                .Include(i => i.Store)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }
        // update db DELETE itemstoreprice
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ItemStorePrices.FindAsync(id);
            if (item != null)
            {
                _context.ItemStorePrices.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Items", new { id= item.ItemId });
        }
    }
}
