using AlexShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlexShop.Controllers
{
    public class CartController : Controller
    {

        public IActionResult Index()
        {
            var product = CartsRepository.TryGetByUserId(Constants.UserId);
            return View(product);
        }



        public IActionResult Add(int productId)
        {
            var product = ProductRepository.TryGetById(productId);
            CartsRepository.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }


        
    }
}