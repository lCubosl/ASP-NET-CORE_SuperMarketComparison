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
        public async Task<IActionResult> Create(int id)
        {
            Console.WriteLine("VIEW for itemstoreprice triggered");
            
            var item = await _context.Items.FindAsync(id);
            
            if (item == null)
                return NotFound();

            var stores = await _context.Stores.ToListAsync();
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
            // check if post is at least  getting triggered
            Console.WriteLine("POST for itemstoreprice triggered");

            if (ModelState.IsValid)
            {
                // checking if model isvalid
                Console.WriteLine("ModelState IS VALID");
                _context.ItemStorePrices.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Items", new { id = model.ItemId });
            }

            // log for validation errors REMOVE LATER
            // logs what fields are empty (ItemId, StoreId, Price, etc...)
            // ex: Key: Item Error: The Item field is required
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                    Console.WriteLine($"Key: {state.Key} Error: {error.ErrorMessage}");
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

    }
}
