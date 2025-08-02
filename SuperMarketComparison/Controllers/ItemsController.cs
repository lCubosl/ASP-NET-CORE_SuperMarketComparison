using Microsoft.AspNetCore.Mvc;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Overview()
        {
            var item = new Item() { Name = "papel higienico" };
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            return Content("id= " + id);
        }
    }
}
