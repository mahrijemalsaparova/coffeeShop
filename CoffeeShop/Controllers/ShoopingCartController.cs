using CoffeeShop.Models.Interfaces;
using CoffeeShop.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCardRepository shoppingCartRepository;
        private IProductRepository productRepository;
        public ShoppingCartController(IShoppingCardRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var items = shoppingCartRepository.GetShoppingCardItems();
            shoppingCartRepository.ShoppingCardItems = items;
            ViewBag.CartTotal = shoppingCartRepository.GetShoppingCartTotal();
            return View(items);
        }

        public RedirectToActionResult AddToShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
            if (product != null)
            {
                shoppingCartRepository.AddToCart(product);
                int cartCount = shoppingCartRepository.GetShoppingCardItems().Count;
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
            if (product != null)
            {
                shoppingCartRepository.RemoveFromCart(product);
                int cartCount = shoppingCartRepository.GetShoppingCardItems().Count;
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("Index");
        }
    }
}