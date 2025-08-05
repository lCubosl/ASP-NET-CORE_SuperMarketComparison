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
        public async Task<IActionResult> Create(int id, [Bind("ItemId, StoreId, Price, LastUpdate")] ItemStorePrice model)
        {
            Console.WriteLine("POST for itemstoreprice triggered");

            if (ModelState.IsValid)
            {
                _context.ItemStorePrices.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Items", new { id = model.ItemId });
            }

            // log for validation errors 
            foreach(var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                    Console.WriteLine($"Key: {state.Key} Error: {error.ErrorMessage}");
            }

            // after submit, item will be set to null and page will throw an error. this is a simple
            // fix to repopulate item for display
            model.Item = await _context.Items.FindAsync(model.ItemId);

            var stores = await _context.Stores.ToListAsync();
            ViewBag.StoreList = new SelectList(stores, "Id", "Name");

            return View(model);
        }
    }
}
