using Microsoft.AspNetCore.Mvc;

namespace SuperMarketComparison.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
