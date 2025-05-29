using AlexShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlexShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int productId)
        {
            var product = ProductRepository.TryGetById(productId);
            return View(product);
        }
    }
}

