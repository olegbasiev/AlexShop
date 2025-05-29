using Microsoft.AspNetCore.Mvc;

namespace AlexShop.Controllers
{
    public class FavouriteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
