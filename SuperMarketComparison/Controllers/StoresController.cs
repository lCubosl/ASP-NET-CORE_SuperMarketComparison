using Microsoft.AspNetCore.Mvc;

namespace SuperMarketComparison.Controllers
{
    public class StoresController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
